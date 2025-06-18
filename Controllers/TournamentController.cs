using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Services.Implementations;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController(ITournamentService tournamentService) : ControllerBase
    {
        private readonly ITournamentService _tournamentService = tournamentService;

        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            var tournaments = await _tournamentService.GetTournamentsAsync();

            return Ok(new { data = tournaments });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTournamentById(Guid id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament is null)
            {
                return NotFound(new { message = "Tournament not found." });
            }

            return Ok(new { data = tournament });
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDto tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tournamentService.CreateTournamentAsync(tournament);
            return StatusCode(StatusCodes.Status201Created, new { message = "Tournament Created." });
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateTournament(Guid id, [FromBody] UpdateTournamentDto tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _tournamentService.UpdateTournamentAsync(id, tournament);
            if (updated == false)
            {
                return NotFound(new { message = "Tournament not updated." });
            }

            return Ok(new { message = "Tournament updated." });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> DeleteTournament(Guid id)
        {
            var result = await _tournamentService.DeleteTournamentAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Tournament not deleted." });
            }

            return NoContent();
        }

        [HttpGet("formats")]
        public async Task<IActionResult> GetTournamentFormats()
        {
            var formats = await _tournamentService.GetTournamentFormatsAsync();
            
            return Ok(new { data = formats});
        }

        [HttpGet("organizer/{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetTournamentByOrganizerId(Guid id)
        {
            var myTournaments = await _tournamentService.GetTournamentByOrganizerIdAsync(id);
            return Ok(new { data = myTournaments});
        }
    }
}
