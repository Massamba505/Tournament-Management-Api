using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/tournaments/{tournamentId:guid}/teams")]
[ApiController]
public class TournamentTeamsController(ITournamentTeamService tournamentTeamService) : ControllerBase
{
    private readonly ITournamentTeamService _tournamentTeamService = tournamentTeamService;

    /// <summary>
    /// Adds a team to a tournament
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> JoinTournament(Guid tournamentId, [FromBody] JoinTournamentDto joinTournamentDto)
    {
        var existing = await _tournamentTeamService.GetTournamentTeamByTeamIdAsync(tournamentId, joinTournamentDto.TeamId);

        if (existing is not null)
        {
            return Conflict(new { message = "Team is already registered in this tournament." });
        }

        await _tournamentTeamService.AddTournamentTeamAsync(tournamentId, joinTournamentDto);
        return Ok(new { message = "Team joined successfully" });
    }

    /// <summary>
    /// Gets a specific team in a tournament by team ID
    /// </summary>
    [HttpGet("{teamId:guid}")]
    public async Task<IActionResult> GetTournamentTeamByTeamId(Guid tournamentId, Guid teamId)
    {
        var team = await _tournamentTeamService.GetTournamentTeamByTeamIdAsync(tournamentId, teamId);

        if (team is null)
        {
            return NotFound(new { message = "Team not found in tournament" });
        }

        return Ok(new { data = team });
    }

    /// <summary>
    /// Gets detailed information of a team in a tournament
    /// </summary>
    [HttpGet("{teamId:guid}/details")]
    public async Task<IActionResult> GetTournamentTeamDetailsByTeamId(Guid tournamentId, Guid teamId)
    {
        var teamDetails = await _tournamentTeamService.GetTournamentTeamDetailsByTeamIdAsync(tournamentId, teamId);

        if (teamDetails is null)
        {
            return NotFound(new { message = "Team not found in tournament" });
        }

        return Ok(new { data = teamDetails });
    }

    /// <summary>
    /// Lists all teams registered in a tournament
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTeamsByTournament(Guid tournamentId)
    {
        var teams = await _tournamentTeamService.GetTournamentTeamsByTournamentIdAsync(tournamentId);
        return Ok(new { data = teams });
    }

    /// <summary>
    /// Removes a team from a tournament
    /// </summary>
    [HttpDelete("{teamId:guid}")]
    [Authorize(Roles = "Organizer,General")]
    public async Task<IActionResult> RemoveTeamFromTournament(Guid tournamentId, Guid teamId)
    {
        var removed = await _tournamentTeamService.RemoveTournamentTeamAsync(tournamentId, teamId);

        if (!removed)
        {
            return NotFound(new { message = "Team not found in tournament" });
        }

        return Ok(new { message = "Team removed from tournament" });
    }

    /// <summary>
    /// Checks if a team is already registered in the tournament
    /// </summary>
    [HttpGet("{teamId:guid}/exists")]
    public async Task<IActionResult> CheckIfTeamInTournament(Guid tournamentId, Guid teamId)
    {
        var exists = await _tournamentTeamService.GetTournamentTeamByTeamIdAsync(tournamentId, teamId);
        if(exists is not null)
        {
            return Ok(new { exists = true });
        }
        return Ok(new { exists = false });
    }
}
