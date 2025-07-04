using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class TeamRepository(TournamentManagerContext context) : ITeamRepository
{
    private readonly TournamentManagerContext _context = context;

    public async Task<IEnumerable<Team>> GetTeamsByUserIdAsync(Guid userId)
    {
        return await _context.Teams
                .Where(t => t.ManagerId == userId)
                .ToListAsync();
    }

    public async Task<Team?> GetTeamByIdAsync(Guid id)
    {
        return await _context.Teams
            .Include(t => t.Manager)
            .Include(t => t.Captain)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateTeamAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTeamAsync(Team team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(Team team)
    {
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTeamStatusAsync(Team team, TeamStatus status)
    {
        team.Status = status;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Team>> GetTeamsByStatusAsync(TeamStatus status)
    {
        return await _context.Teams
            .Where(t => t.Status == status)
            .Include(t => t.Manager)
            .Include(t => t.Captain)
            .ToListAsync();
    }
}
