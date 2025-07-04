using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class TournamentTeamRepository(TournamentManagerContext context) : ITournamentTeamRepository
{
    private readonly TournamentManagerContext _context = context;

    public async Task<TournamentTeam?> GetTournamentTeamByTeamIdAsync(Guid tournamentId, Guid id)
    {
        return await _context.TournamentTeams
            .Include(tt => tt.Team)
                .ThenInclude(team => team.Manager)
            .Include(tt => tt.Team)
                .ThenInclude(team => team.Captain)
            .FirstOrDefaultAsync(tt => tt.TeamId == id && tt.TournamentId == tournamentId);
    }

    public async Task<IEnumerable<TournamentTeam>> GetTournamentTeamsByTournamentIdAsync(Guid tournamentId)
    {

        return await _context.TournamentTeams
            .Where(tt => tt.TournamentId == tournamentId)
            .Include(tt => tt.Team)
                .ThenInclude(team => team.Manager)
            .Include(tt => tt.Team)
                .ThenInclude(team => team.Captain)
            .ToListAsync();
    }

    public async Task AddTournamentTeamAsync(TournamentTeam tournamentTeam)
    {
        _context.TournamentTeams.Add(tournamentTeam);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTournamentTeamAsync(TournamentTeam tournamentTeam)
    {
        _context.TournamentTeams.Update(tournamentTeam);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTournamentTeamAsync(TournamentTeam tournamentTeam)
    {
        _context.TournamentTeams.Remove(tournamentTeam);
        await _context.SaveChangesAsync();
    }
}
