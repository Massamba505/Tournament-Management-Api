using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces;

public interface IPlayerStatRepository
{
    Task<IEnumerable<PlayerStat>> GetPlayerStatsByMatchIdAsync(Guid matchId);
    Task<IEnumerable<PlayerStat>> GetPlayerStatsByPlayerIdAsync(Guid playerId);
    Task<PlayerStat?> GetPlayerStatByPlayerAndMatchAsync(Guid playerId, Guid matchId);
    Task CreatePlayerStatAsync(PlayerStat playerStat);
    Task UpdatePlayerStatAsync(PlayerStat playerStat);
    Task DeletePlayerStatAsync(PlayerStat playerStat);
}
