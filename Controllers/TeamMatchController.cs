using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.TeamMatches;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class TeamMatchesController(ITeamMatchService teamMatchService) : ControllerBase
    {
        private readonly ITeamMatchService _teamMatchService = teamMatchService;

        [HttpGet("tournament/{tournamentId:guid}")]
        public async Task<IActionResult> GetMatchesByTournament(Guid tournamentId)
        {
            var matches = await _teamMatchService.GetMatchesByTournamentIdAsync(tournamentId);
            return Ok(new { data = matches });
        }

        [HttpGet("{matchId:guid}")]
        public async Task<IActionResult> GetMatchById(Guid matchId)
        {
            var match = await _teamMatchService.GetMatchByIdAsync(matchId);
            if (match is null)
            {
                return NotFound(new { message = "Match not found" });
            }

            return Ok(new { data = match });
        }

        [HttpGet("team/{teamId:guid}")]
        public async Task<IActionResult> GetMatchesByTeam(Guid teamId)
        {
            var matches = await _teamMatchService.GetMatchesByTeamIdAsync(teamId);
            return Ok(new { data = matches });
        }

        [HttpGet("tournament/{tournamentId:guid}/status/{status}")]
        public async Task<IActionResult> GetMatchesByStatus(Guid tournamentId, MatchStatus status)
        {
            var matches = await _teamMatchService.GetMatchesByStatusAsync(tournamentId, status);
            return Ok(new { data = matches });
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreateMatch([FromBody] MatchCreateDto createMatch)
        {
            await _teamMatchService.CreateMatchAsync(createMatch);
            return StatusCode(StatusCodes.Status201Created, new { message = "Match created successfully" });
        }

        [HttpPut("{matchId:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateMatch(Guid matchId, [FromBody] MatchUpdateDto updateMatch)
        {
            var result = await _teamMatchService.UpdateMatchAsync(matchId, updateMatch);
            if (!result)
            {
                return NotFound(new { message = "Match not found" });
            }

            return Ok(new { message = "Match updated successfully" });
        }

        [HttpPatch("{matchId:guid}/status")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateMatchStatus(Guid matchId, [FromBody] UpdateMatchStatusDto statusDto)
        {
            var result = await _teamMatchService.UpdateMatchStatusAsync(matchId, statusDto.Status);
            if (!result)
            {
                return NotFound(new { message = "Match not found" });
            }

            return Ok(new { message = "Match status updated successfully" });
        }

        [HttpDelete("{matchId:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> DeleteMatch(Guid matchId)
        {
            var result = await _teamMatchService.DeleteMatchAsync(matchId);
            if (!result)
            {
                return NotFound(new { message = "Match not found" });
            }

            return Ok(new { message = "Match deleted successfully" });
        }
    }
}
