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
        private readonly ITournamentService _service;

        public TournamentController(ITournamentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _service.GetAllAsync();

            return Ok(new { data = tournaments });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tournament = await _service.GetByIdAsync(id);
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

            var created = await _service.CreateAsync(dto);

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

            var updated = await _service.UpdateAsync(id, dto);
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
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Tournament not found." });
            }

            return NoContent();
        }
    }
}
