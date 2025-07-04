using Tournament.Management.API.Models.DTOs.PlayerStats;

namespace Tournament.Management.API.Services.Interfaces;

public interface IPlayerStatService
{
    Task<IEnumerable<PlayerStatDto>> GetPlayerStatsByMatchIdAsync(Guid matchId);
    Task<IEnumerable<PlayerStatDto>> GetPlayerStatsByPlayerIdAsync(Guid playerId);
    Task<PlayerStatDto?> GetPlayerStatByPlayerAndMatchAsync(Guid playerId, Guid matchId);
    Task CreatePlayerStatAsync(PlayerStatCreateDto createStat);
    Task<bool> UpdatePlayerStatAsync(Guid playerId, Guid matchId, PlayerStatUpdateDto updateStat);
    Task<bool> DeletePlayerStatAsync(Guid playerId, Guid matchId);
}
