using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournament.Management.API.Models.DTOs.Team;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController(ITeamService teamService) : ControllerBase
    {
        private readonly ITeamService _teamService = teamService;

        [HttpGet]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> GetMyTeams()
        {
            var userId = GetUserId();
            var teams = await _teamService.GetMyTeamsAsync(userId);
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(Guid id)
        {
            var userId = GetUserId();
            var team = await _teamService.GetByIdAsync(id, userId);
            if (team == null)
            {
                return NotFound(new { success = false, message = "Team not found" });
            }

            return Ok(team);
        }

        [HttpPost]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> CreateTeam([FromBody] TeamCreateDto teamDto)
        {
            var userId = GetUserId();
            await _teamService.CreateAsync(userId,teamDto);

            return StatusCode(StatusCodes.Status201Created,new { message = "Team Created"});
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> UpdateTeam(Guid id, [FromBody] TeamUpdateDto dto)
        {
            var userId = GetUserId();
            var result = await _teamService.UpdateAsync(id, dto, userId);
            if (!result)
            {
                return NotFound(new { success = false, message = "Team not updated" });
            }

            return Ok(new { success = true, message = "Team updated" });
        }

        [HttpPatch("{id}/deactivate")]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> DeactivateTeam(Guid id)
        {
            var userId = GetUserId();
            var result = await _teamService.DeactivateAsync(id, userId);
            if (!result)
            {
                return NotFound(new { success = false, message = "Team not deactivated" });
            }

            return Ok(new { success = true, message = "Team deactivated" });
        }

        [HttpPatch("{id}/activate")]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> ActivateTeam(Guid id)
        {
            var userId = GetUserId();
            var result = await _teamService.ActivateAsync(id, userId);
            if (!result)
            {
                return NotFound(new { success = false, message = "Team not activated" });
            }

            return Ok(new { success = true, message = "Team activated" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "General")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var userId = GetUserId();
            var result = await _teamService.DeleteAsync(id, userId);
            if (!result)
            {
                return NotFound(new { success = false, message = "Team not deleted" });
            }

            return Ok(new { success = true, message = "Team deleted" });
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}
