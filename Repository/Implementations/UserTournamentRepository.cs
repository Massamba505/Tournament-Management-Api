using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class UserTournamentRepository(TournamentManagerContext context) : ITournamentRepository
{
    private readonly TournamentManagerContext _context = context;

    public async Task<IEnumerable<UserTournament>> GetTournamentsAsync()
    {
        return await _context.UserTournaments
            .Include(x => x.Format)
            .ToListAsync();
    }

    public async Task<UserTournament?> GetTournamentByIdAsync(Guid tournamentId)
    {
        return await _context.UserTournaments
            .Include(x => x.Organizer)
            .Include(x => x.Format)
            .FirstOrDefaultAsync(x => x.Id == tournamentId);
    }

    public async Task CreateTournamentAsync(UserTournament tournament)
    {
        _context.UserTournaments.Add(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTournamentAsync(UserTournament tournament)
    {
        _context.UserTournaments.Update(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTournamentAsync(UserTournament tournament)
    {
        _context.UserTournaments.Remove(tournament);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserTournament>> GetTournamentByOrganizerIdAsync(Guid userId)
    {
        return await _context.UserTournaments
            .Where(x => x.OrganizerId == userId)
            .Include(x => x.Format)
            .ToListAsync();
    }
}
