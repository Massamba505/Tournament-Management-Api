using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.Tournament;
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
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _tournamentService.GetAllAsync();

            return Ok(new { data = tournaments });
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetById(Guid id)
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
        public async Task<IActionResult> Create([FromBody] CreateTournamentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _tournamentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.TournamentId }, created);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTournamentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _tournamentService.UpdateAsync(id, dto);
            if (updated == null)
            {
                return NotFound(new { message = "Tournament not found." });
            }

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tournamentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Tournament not found." });
            }

            return NoContent();
        }
    }
}
