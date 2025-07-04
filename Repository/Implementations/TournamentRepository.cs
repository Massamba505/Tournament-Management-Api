using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations
{
    public class TournamentRepository(TournamentManagerContext context) : ITournamentRepository
    {
        private readonly TournamentManagerContext _context = context;

        public async Task<IEnumerable<UserTournament>> GetTournamentsAsync()
        {
            return await _context.Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.TournamentTeams)
                    .ThenInclude(tt => tt.Team)
                .Include(t => t.Matches)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<UserTournament?> GetTournamentByIdAsync(Guid id)
        {
            return await _context.Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.TournamentTeams)
                    .ThenInclude(tt => tt.Team)
                .Include(t => t.Matches)
                    .ThenInclude(m => m.HomeTeam)
                .Include(t => t.Matches)
                    .ThenInclude(m => m.AwayTeam)
                .Include(t => t.Matches)
                    .ThenInclude(m => m.PlayerStats)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateTournamentAsync(UserTournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTournamentAsync(UserTournament tournament)
        {
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTournamentAsync(UserTournament tournament)
        {
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserTournament>> GetTournamentByOrganizerIdAsync(Guid userId)
        {
            return await _context.Tournaments
                .Where(t => t.OrganizerId == userId)
                .Include(t => t.Organizer)
                .Include(t => t.TournamentTeams)
                    .ThenInclude(tt => tt.Team)
                .Include(t => t.Matches)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserTournament>> GetTournamentsByStatusAsync(TournamentStatus status)
        {
            return await _context.Tournaments
                .Where(t => t.Status == status)
                .Include(t => t.Organizer)
                .Include(t => t.TournamentTeams)
                    .ThenInclude(tt => tt.Team)
                .Include(t => t.Matches)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
