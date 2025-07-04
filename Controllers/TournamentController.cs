using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController(ITournamentService tournamentService, ITournamentFormatService formatService) : ControllerBase
    {
        private readonly ITournamentService _tournamentService = tournamentService;
        private readonly ITournamentFormatService _formatService = formatService;

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

        [HttpGet("{id:guid}/details")]
        public async Task<IActionResult> GetTournamentDetailsById(Guid id)
        {
            var tournament = await _tournamentService.GetTournamentDetailsByIdAsync(id);
            if (tournament is null)
            {
                return NotFound(new { message = "Tournament not found." });
            }

            return Ok(new { data = tournament });
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreateTournament([FromBody] TournamentCreateDto tournament)
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
        public async Task<IActionResult> UpdateTournament(Guid id, [FromBody] TournamentUpdateDto tournament)
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

        [HttpPatch("{id:guid}/status")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateTournamentStatus(Guid id, [FromBody] UpdateTournamentStatusDto statusDto)
        {
            var updated = await _tournamentService.UpdateTournamentStatusAsync(id, statusDto.Status);
            if (updated == false)
            {
                return NotFound(new { message = "Tournament not found or status not updated." });
            }

            return Ok(new { message = "Tournament status updated." });
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
            var formats = await _formatService.GetFormatsAsync();
            
            return Ok(new { data = formats});
        }

        [HttpGet("organizer/{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetTournamentsByOrganizerId(Guid id)
        {
            var myTournaments = await _tournamentService.GetTournamentsByOrganizerIdAsync(id);
            return Ok(new { data = myTournaments });
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTournamentsByStatus(TournamentStatus status)
        {
            var tournaments = await _tournamentService.GetTournamentsByStatusAsync(status);
            return Ok(new { data = tournaments });
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}
