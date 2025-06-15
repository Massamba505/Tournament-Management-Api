using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations
{
    public class TeamRepository(TournamentManagerContext context) : ITeamRepository
    {
        private readonly TournamentManagerContext _context = context;

        public async Task<IEnumerable<Team>> GetMyTeamsAsync(Guid userId)
        {
            return await _context.Teams
                    .AsNoTracking()
                    .Where(t => t.ManagerId == userId)
                    .ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == id && t.ManagerId == userId);
        }

        public async Task CreateAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateAsync(Team team)
        {
            team.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task ActivateAsync(Team team)
        {
            team.IsActive = true;
            await _context.SaveChangesAsync();
        }
    }
}
