using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/tournaments/{tournamentId:guid}/teams")]
    [ApiController]
    public class TournamentTeamsController(ITournamentTeamService tournamentTeamService) : ControllerBase
    {
        private readonly ITournamentTeamService _tournamentTeamService = tournamentTeamService;

        [HttpPost]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> JoinTournament(Guid tournamentId, [FromBody] JoinTournamentDto joinTournamentDto)
        {
            await _tournamentTeamService.AddTournamentTeamAsync(tournamentId, joinTournamentDto);
            return Ok(new {message = "Team joined successfully"});
        }

        [HttpGet("{teamId:guid}")]
        public async Task<IActionResult> GetTournamentTeamByTeamId(Guid tournamentId, Guid teamId)
        {
            var team = await _tournamentTeamService.GetTournamentTeamByTeamIdAsync(tournamentId, teamId);
            if(team is null)
            {
                return NotFound(new { message = "Team not found in tournament" });
            }

            return Ok(new { data = team });
        }

        [HttpGet("{teamId:guid}/details")]
        public async Task<IActionResult> GetTournamentTeamDetailsByTeamId(Guid tournamentId, Guid teamId)
        {
            var teamDetails = await _tournamentTeamService.GetTournamentTeamDetailsByTeamIdAsync(tournamentId, teamId);
            if(teamDetails is null)
            {
                return NotFound(new { message = "Team not found in tournament" });
            }

            return Ok(new { data = teamDetails });
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamsByTournament(Guid tournamentId)
        {
            var teams = await _tournamentTeamService.GetTournamentTeamsByTournamentIdAsync(tournamentId);
            return Ok(new { data = teams });
        }

        [HttpDelete("{teamId:guid}")]
        [Authorize(Roles = "Organizer,General")]
        public async Task<IActionResult> RemoveTeamFromTournament(Guid tournamentId, Guid teamId)
        {
            var removed = await _tournamentTeamService.RemoveTournamentTeamAsync(tournamentId, teamId);
            if (!removed)
            {
                return NotFound(new { message = "Team not found in tournament" });
            }
            return Ok(new {message = "Team removed from tournament"});
        }
    }
}
