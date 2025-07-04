using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class PlayerStatRepository(TournamentManagerContext context) : IPlayerStatRepository
{
    private readonly TournamentManagerContext _context = context;

    public async Task<IEnumerable<PlayerStat>> GetPlayerStatsByMatchIdAsync(Guid matchId)
    {
        return await _context.PlayerStats
            .Where(ps => ps.MatchId == matchId)
            .Include(ps => ps.Player)
            .Include(ps => ps.Match)
            .ToListAsync();
    }

    public async Task<IEnumerable<PlayerStat>> GetPlayerStatsByPlayerIdAsync(Guid playerId)
    {
        return await _context.PlayerStats
            .Where(ps => ps.PlayerId == playerId)
            .Include(ps => ps.Match)
            .ToListAsync();
    }

    public async Task<PlayerStat?> GetPlayerStatByPlayerAndMatchAsync(Guid playerId, Guid matchId)
    {
        return await _context.PlayerStats
            .Include(ps => ps.Player)
            .Include(ps => ps.Match)
            .FirstOrDefaultAsync(ps => ps.PlayerId == playerId && ps.MatchId == matchId);
    }

    public async Task CreatePlayerStatAsync(PlayerStat playerStat)
    {
        _context.PlayerStats.Add(playerStat);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePlayerStatAsync(PlayerStat playerStat)
    {
        _context.PlayerStats.Update(playerStat);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePlayerStatAsync(PlayerStat playerStat)
    {
        _context.PlayerStats.Remove(playerStat);
        await _context.SaveChangesAsync();
    }
}
