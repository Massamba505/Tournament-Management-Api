using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.PlayerStats;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/player-stats")]
[ApiController]
public class PlayerStatsController(IPlayerStatService playerStatService) : ControllerBase
{
    private readonly IPlayerStatService _playerStatService = playerStatService;

    /// <summary>
    /// Get all player stats for a specific match
    /// </summary>
    [HttpGet("match/{matchId:guid}")]
    public async Task<IActionResult> GetStatsByMatch(Guid matchId)
    {
        var stats = await _playerStatService.GetPlayerStatsByMatchIdAsync(matchId);
        return Ok(new { data = stats });
    }

    /// <summary>
    /// Get all stats for a specific player
    /// </summary>
    [HttpGet("player/{playerId:guid}")]
    public async Task<IActionResult> GetStatsByPlayer(Guid playerId)
    {
        var stats = await _playerStatService.GetPlayerStatsByPlayerIdAsync(playerId);
        return Ok(new { data = stats });
    }

    /// <summary>
    /// Get stat for a player in a specific match
    /// </summary>
    [HttpGet("player/{playerId:guid}/match/{matchId:guid}")]
    public async Task<IActionResult> GetStatByPlayerAndMatch(Guid playerId, Guid matchId)
    {
        var stat = await _playerStatService.GetPlayerStatByPlayerAndMatchAsync(playerId, matchId);
        if (stat == null)
        {
            return NotFound(new { message = "Player stat not found" });
        }

        return Ok(new { data = stat });
    }

    /// <summary>
    /// Create a new player stat
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> CreatePlayerStat([FromBody] PlayerStatCreateDto createDto)
    {
        await _playerStatService.CreatePlayerStatAsync(createDto);
        return StatusCode(StatusCodes.Status201Created, new { message = "Player stat created successfully" });
    }

    /// <summary>
    /// Update a player stat entry for a specific match
    /// </summary>
    [HttpPut("player/{playerId:guid}/match/{matchId:guid}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> UpdatePlayerStat(Guid playerId, Guid matchId, [FromBody] PlayerStatUpdateDto updateDto)
    {
        var result = await _playerStatService.UpdatePlayerStatAsync(playerId, matchId, updateDto);
        if (!result)
        {
            return NotFound(new { message = "Player stat not found" });
        }

        return Ok(new { message = "Player stat updated successfully" });
    }

    /// <summary>
    /// Delete a player stat for a specific match
    /// </summary>
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
