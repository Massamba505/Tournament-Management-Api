using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController(ITeamService teamService) : ControllerBase
{
    private readonly ITeamService _teamService = teamService;

    [HttpGet]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> GetMyTeams()
    {
        var userId = GetUserId();
        var teams = await _teamService.GetMyTeamsAsync(userId);
        return Ok(new { data = teams});
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(Guid id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound(new { message = "Team not found" });
        }

        return Ok(new { data = team });
    }

    [HttpPost]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> CreateTeam([FromBody] TeamCreateDto teamDto)
    {
        var userId = GetUserId();
        await _teamService.CreateTeamAsync(userId,teamDto);

        return StatusCode(StatusCodes.Status201Created, new { message = "Team Created"});
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> UpdateTeam(Guid id, [FromBody] TeamUpdateDto team)
    {
        var result = await _teamService.UpdateTeamAsync(id, team);
        if (!result)
        {
            return NotFound(new { success = false, message = "Team not updated" });
        }

        return Ok(new { message = "Team updated" });
    }

    [HttpPatch("{id}/deactivate")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> DeactivateTeam(Guid id)
    {
        var result = await _teamService.UpdateTeamStatusAsync(id, TeamStatus.Inactive);
        if (!result)
        {
            return NotFound(new { success = false, message = "Team not deactivated" });
        }

        return Ok(new { message = "Team deactivated" });
    }

    [HttpPatch("{id}/activate")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> ActivateTeam(Guid id)
    {
        var result = await _teamService.UpdateTeamStatusAsync(id, TeamStatus.Active);
        if (!result)
        {
            return NotFound(new { success = false, message = "Team not activated" });
        }

        return Ok(new { message = "Team activated" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> DeleteTeam(Guid id)
    {
        var result = await _teamService.DeleteTeamAsync(id);
        if (!result)
        {
            return NotFound(new { success = false, message = "Team not deleted" });
        }

        return Ok(new { message = "Team deleted" });
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
