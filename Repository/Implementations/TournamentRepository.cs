using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentManagerContext _context;

        public TournamentRepository(TournamentManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserTournament>> GetAllAsync()
        {
            return await _context.UserTournaments
                .Include(x => x.Format)
                .ToListAsync();
        }

        public async Task<UserTournament?> GetByIdAsync(Guid tournamentId)
        {
            return await _context.UserTournaments
                .Include(x => x.Format)
                .FirstOrDefaultAsync(x => x.Id == tournamentId);
        }

        public async Task CreateAsync(UserTournament tournament)
        {
            _context.UserTournaments.Add(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserTournament tournament)
        {
            _context.UserTournaments.Update(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserTournament tournament)
        {
            _context.UserTournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }
    }
}
