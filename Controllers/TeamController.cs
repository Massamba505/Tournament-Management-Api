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

    [HttpGet("details/{id:guid}")]
    public async Task<IActionResult> GetTeamDetails(Guid id)
    {
        var teamDetails = await _teamService.GetTeamDetailsByIdAsync(id);
        if (teamDetails == null)
        {
            return NotFound(new { message = "Team not found" });
        }

        return Ok(new { data = teamDetails });
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetTeamsByStatus(TeamStatus status)
    {
        var teams = await _teamService.GetTeamsByStatusAsync(status);
        return Ok(new { data = teams });
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTeams([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
        {
            return BadRequest(new { message = "Search query must be at least 3 characters." });
        }

        var allTeams = await _teamService.GetTeamsByStatusAsync(TeamStatus.Active);
        var filteredTeams = allTeams.Where(t => 
            t.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
        
        return Ok(new { data = filteredTeams });
    }

    [HttpGet("{id:guid}/matches")]
    public async Task<IActionResult> GetTeamMatches(Guid id, [FromServices] ITeamMatchService teamMatchService)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound(new { message = "Team not found" });
        }

        var matches = await teamMatchService.GetMatchesByTeamIdAsync(id);
        return Ok(new { data = matches });
    }

    [HttpGet("{id:guid}/statistics")]
    public async Task<IActionResult> GetTeamStatistics(Guid id, [FromServices] ITeamMatchService teamMatchService, [FromServices] IPlayerStatService playerStatService)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound(new { message = "Team not found" });
        }

        var teamDetails = await _teamService.GetTeamDetailsByIdAsync(id);
        var matches = await teamMatchService.GetMatchesByTeamIdAsync(id);
        
        var matchesPlayed = matches.Count();
        var wins = matches.Count(m => 
            (m.HomeTeam.Id == id && m.HomeScore > m.AwayScore) || 
            (m.AwayTeam.Id == id && m.AwayScore > m.HomeScore));
        var losses = matches.Count(m => 
            (m.HomeTeam.Id == id && m.HomeScore < m.AwayScore) || 
            (m.AwayTeam.Id == id && m.AwayScore < m.HomeScore));
        var draws = matches.Count(m => m.HomeScore == m.AwayScore);

        var playerStats = new List<object>();
        if (teamDetails != null)
        {
            foreach (var member in teamDetails.Members)
            {
                var stats = await playerStatService.GetPlayerStatsByPlayerIdAsync(member.UserId);
                if (stats.Any())
                {
                    playerStats.Add(new
                    {
                        playerId = member.UserId,
                        playerName = $"{member.FullName}",
                        isCaptain = member.IsCaptain,
                        stats = stats
                    });
                }
            }
        }

        return Ok(new { 
            data = new {
                teamId = id,
                teamName = team.Name,
                matchesPlayed,
                wins,
                losses,
                draws,
                winPercentage = matchesPlayed > 0 ? (wins * 100.0 / matchesPlayed) : 0,
                playerStats
            } 
        });
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
