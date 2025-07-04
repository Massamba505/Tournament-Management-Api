using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class TeamMatchRepository(TournamentManagerContext context) : ITeamMatchRepository
{
    private readonly TournamentManagerContext _context = context;

    public async Task<IEnumerable<TeamMatch>> GetMatchesByTournamentIdAsync(Guid tournamentId)
    {
        return await _context.TeamMatches
            .Where(m => m.TournamentId == tournamentId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }

    public async Task<TeamMatch?> GetMatchByIdAsync(Guid matchId)
    {
        return await _context.TeamMatches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Tournament)
            .Include(m => m.PlayerStats)
            .FirstOrDefaultAsync(m => m.Id == matchId);
    }

    public async Task<IEnumerable<TeamMatch>> GetMatchesByTeamIdAsync(Guid teamId)
    {
        return await _context.TeamMatches
            .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Tournament)
            .ToListAsync();
    }

    public async Task<IEnumerable<TeamMatch>> GetMatchesByStatusAsync(Guid tournamentId, MatchStatus status)
    {
        return await _context.TeamMatches
            .Where(m => m.TournamentId == tournamentId && m.Status == status)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }

    public async Task CreateMatchAsync(TeamMatch match)
    {
        _context.TeamMatches.Add(match);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMatchAsync(TeamMatch match)
    {
        _context.TeamMatches.Update(match);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMatchAsync(TeamMatch match)
    {
        _context.TeamMatches.Remove(match);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMatchStatusAsync(TeamMatch match, MatchStatus status)
    {
        match.Status = status;
        await _context.SaveChangesAsync();
    }
}
