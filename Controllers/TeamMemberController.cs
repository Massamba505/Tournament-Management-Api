using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.TeamMembers;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/teams/{teamId:guid}/members")]
[ApiController]
public class TeamMembersController(ITeamMemberService teamMemberService) : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService = teamMemberService;

    /// <summary>
    /// Get all members of a specific team
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTeamMembers(Guid teamId)
    {
        var teamMembers = await _teamMemberService.GetTeamMembersAsync(teamId);
        return Ok(new { data = teamMembers });
    }

    /// <summary>
    /// Get a specific team member by userId
    /// </summary>
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetTeamMember(Guid teamId, Guid userId)
    {
        var member = await _teamMemberService.GetTeamMemberAsync(teamId, userId);
        if (member == null)
        {
            return NotFound(new { message = "Team member not found" });
        }

        return Ok(new { data = member });
    }

    /// <summary>
    /// Add a player to the team
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> AddTeamMember(Guid teamId, [FromBody] AddTeamMemberDto teamMember)
    {
        await _teamMemberService.AddTeamMemberAsync(teamId, teamMember);
        return Ok(new { message = "Player added successfully" });
    }

    /// <summary>
    /// Remove a player from the team
    /// </summary>
    [HttpDelete("{userId:guid}")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> RemoveTeamMember(Guid teamId, Guid userId)
    {
        var result = await _teamMemberService.RemoveTeamMemberAsync(teamId, userId);
        if (!result)
        {
            return NotFound(new { message = "Player not found" });
        }

        return Ok(new { message = "Player removed successfully" });
    }

    /// <summary>
    /// Assign a player as the team captain
    /// </summary>
    [HttpPatch("{userId:guid}/captain/assign")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> AssignTeamCaptain(Guid teamId, Guid userId)
    {
        var result = await _teamMemberService.AssignTeamCaptainAsync(teamId, userId);
        if (!result)
        {
            return NotFound(new { message = "Player not found" });
        }

        return Ok(new { message = "Captain assigned successfully" });
    }

    /// <summary>
    /// Update a team member’s role
    /// </summary>
    [HttpPatch("{userId:guid}/type")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> UpdateMemberType(Guid teamId, Guid userId, [FromBody] UpdateMemberTypeDto dto)
    {
        var result = await _teamMemberService.UpdateMemberTypeAsync(teamId, userId, dto.MemberType);
        if (!result)
        {
            return NotFound(new { message = "Player not found" });
        }

        return Ok(new { message = "Member type updated successfully" });
    }
}
