using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournament.Management.API.Models.DTOs.TeamInvitations;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/team-invitations")]
[ApiController]
[Authorize]
public class TeamInvitationsController(ITeamInvitationService teamInvitationService, ITeamService teamService, ITeamMemberService teamMemberService) : ControllerBase
{
    private readonly ITeamInvitationService _teamInvitationService = teamInvitationService;
    private readonly ITeamService _teamService = teamService;
    private readonly ITeamMemberService _teamMemberService = teamMemberService;

    [HttpGet("team/{teamId:guid}")]
    public async Task<IActionResult> GetTeamInvitations(Guid teamId)
    {
        var invitations = await _teamInvitationService.GetTeamInvitationsAsync(teamId);
        return Ok(new { data = invitations });
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserInvitations()
    {
        var userId = GetUserId();
        var invitations = await _teamInvitationService.GetUserInvitationsAsync(userId);
        return Ok(new { data = invitations });
    }

    [HttpGet("{invitationId:guid}")]
    public async Task<IActionResult> GetInvitationById(Guid invitationId)
    {
        var invitation = await _teamInvitationService.GetInvitationByIdAsync(invitationId);
        if (invitation == null)
        {
            return NotFound(new { message = "Invitation not found" });
        }

        return Ok(new { data = invitation });
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvitation([FromBody] TeamInvitationCreateDto invitationDto)
    {
        var currentUserId = GetUserId();
        
        var team = await _teamService.GetTeamByIdAsync(invitationDto.TeamId);
        if (team == null)
        {
            return NotFound(new { message = "Team not found" });
        }

        if (team.Manager.Id != currentUserId && team.Captain?.Id != currentUserId)
        {
            return Forbid();
        }

        var invitation = invitationDto with { InvitedByUserId = currentUserId };
        var invitationId = await _teamInvitationService.CreateInvitationAsync(invitation);
        
        return StatusCode(StatusCodes.Status201Created, new { 
            message = "Invitation sent successfully",
            invitationId
        });
    }

    [HttpPatch("{invitationId:guid}/respond")]
    public async Task<IActionResult> RespondToInvitation(Guid invitationId, [FromBody] TeamInvitationResponseDto responseDto)
    {
        var userId = GetUserId();
        var invitation = await _teamInvitationService.GetInvitationByIdAsync(invitationId);
        
        if (invitation == null)
        {
            return NotFound(new { message = "Invitation not found" });
        }

        if (invitation.InvitedUserId != userId)
        {
            return Forbid();
        }

        var result = await _teamInvitationService.RespondToInvitationAsync(invitationId, responseDto.Status);
        if (!result)
        {
            return BadRequest(new { message = "Could not process invitation response" });
        }

        if (responseDto.Status == InvitationStatus.Accepted)
        {
            await _teamMemberService.AddTeamMemberAsync(invitation.TeamId, new Models.DTOs.TeamMembers.AddTeamMemberDto(userId, invitation.TeamId));
            return Ok(new { message = "Invitation accepted and you have been added to the team" });
        }

        return Ok(new { message = "Invitation response recorded" });
    }

    [HttpDelete("{invitationId:guid}/cancel")]
    public async Task<IActionResult> CancelInvitation(Guid invitationId)
    {
        var userId = GetUserId();
        var invitation = await _teamInvitationService.GetInvitationByIdAsync(invitationId);
        
        if (invitation == null)
        {
            return NotFound(new { message = "Invitation not found" });
        }

        var team = await _teamService.GetTeamByIdAsync(invitation.TeamId);
        if (invitation.InvitedByUserId != userId && team?.Manager.Id != userId)
        {
            return Forbid();
        }

        var result = await _teamInvitationService.CancelInvitationAsync(invitationId);
        if (!result)
        {
            return BadRequest(new { message = "Could not cancel invitation" });
        }

        return Ok(new { message = "Invitation canceled" });
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
