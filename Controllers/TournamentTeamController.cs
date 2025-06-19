using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentTeam;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/tournaments/{tournamentId:guid}/teams")]
    [ApiController]
    public class TournamentTeamsController(ITournamentTeamService tournamentTeamService) : ControllerBase
    {
        private readonly ITournamentTeamService _tournamentTeamService = tournamentTeamService;

        [HttpPost]
        public async Task<IActionResult> JoinTournament(Guid tournamentId, [FromBody] JoinTournamentDto joinTournamentDto)
        {
            await _tournamentTeamService.AddTournamentTeamAsync(tournamentId, joinTournamentDto);
            return Ok(new {message = "Team joined successfull"});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournamentTeamByTeamId(Guid tournamentId, Guid id)
        {
            var team = await _tournamentTeamService.GetTournamentTeamByTeamIdAsync(tournamentId, id);
            if(team is null)
            {
                return NotFound();
            }

            return Ok(new { data = team });
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamsByTournament(Guid tournamentId)
        {
            var teams = await _tournamentTeamService.GetTournamentTeamsByTournamentIdAsync(tournamentId);
            return Ok(new { data = teams });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTeamFromTournament(Guid tournamentId, Guid id)
        {
            var removed = await _tournamentTeamService.RemoveTournamentTeamAsync(tournamentId, id);
            if (!removed)
            {
                return NotFound();
            }
            return Ok(new {message = "Team removed"});
        }
    }
}
