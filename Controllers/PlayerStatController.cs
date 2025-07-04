using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.PlayerStats;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/player-stats")]
    [ApiController]
    public class PlayerStatsController(IPlayerStatService playerStatService) : ControllerBase
    {
        private readonly IPlayerStatService _playerStatService = playerStatService;

        [HttpGet("match/{matchId:guid}")]
        public async Task<IActionResult> GetStatsByMatch(Guid matchId)
        {
            var stats = await _playerStatService.GetPlayerStatsByMatchIdAsync(matchId);
            return Ok(new { data = stats });
        }

        [HttpGet("player/{playerId:guid}")]
        public async Task<IActionResult> GetStatsByPlayer(Guid playerId)
        {
            var stats = await _playerStatService.GetPlayerStatsByPlayerIdAsync(playerId);
            return Ok(new { data = stats });
        }

        [HttpGet("player/{playerId:guid}/match/{matchId:guid}")]
        public async Task<IActionResult> GetStatByPlayerAndMatch(Guid playerId, Guid matchId)
        {
            var stat = await _playerStatService.GetPlayerStatByPlayerAndMatchAsync(playerId, matchId);
            if (stat is null)
            {
                return NotFound(new { message = "Player stat not found" });
            }

            return Ok(new { data = stat });
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreatePlayerStat([FromBody] PlayerStatCreateDto createStat)
        {
            await _playerStatService.CreatePlayerStatAsync(createStat);
            return StatusCode(StatusCodes.Status201Created, new { message = "Player stat created successfully" });
        }

        [HttpPut("player/{playerId:guid}/match/{matchId:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdatePlayerStat(Guid playerId, Guid matchId, [FromBody] PlayerStatUpdateDto updateStat)
        {
            var result = await _playerStatService.UpdatePlayerStatAsync(playerId, matchId, updateStat);
            if (!result)
            {
                return NotFound(new { message = "Player stat not found" });
            }

            return Ok(new { message = "Player stat updated successfully" });
        }

        [HttpDelete("player/{playerId:guid}/match/{matchId:guid}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> DeletePlayerStat(Guid playerId, Guid matchId)
        {
            var result = await _playerStatService.DeletePlayerStatAsync(playerId, matchId);
            if (!result)
            {
                return NotFound(new { message = "Player stat not found" });
            }

            return Ok(new { message = "Player stat deleted successfully" });
        }
    }
}
