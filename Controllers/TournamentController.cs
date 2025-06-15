using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Services.Implementations;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTournaments()
        {
            var tournaments = await _tournamentService.GetAllAsync();

            return Ok(new { data = tournaments });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTournamentById(Guid id)
        {
            var tournament = await _tournamentService.GetByIdAsync(id);
            if (tournament is null)
            {
                return NotFound(new { message = "Tournament not found." });
            }

            return Ok(tournament);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDto tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tournamentService.CreateAsync(tournament);
            return StatusCode(StatusCodes.Status201Created, new {message = "Tournament Created."});
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateTournament(Guid id, [FromBody] UpdateTournamentDto tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _tournamentService.UpdateAsync(id, tournament);
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
            var result = await _tournamentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Tournament not deleted." });
            }

            return NoContent();
        }

        [HttpGet("formats")]
        public async Task<IActionResult> GetTournamentFormats()
        {
            var formats = await _tournamentService.GetFormatsAsync();
            
            return Ok(formats);
        }
    }
}
