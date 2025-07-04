using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations
{
    public class TeamMemberRepository(TournamentManagerContext context) : ITeamMemberRepository
    {
        private readonly TournamentManagerContext _context = context;

        public async Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(Guid teamId)
        {
            return await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Include(tm => tm.User)
                .Include(tm => tm.Team)
                .ToListAsync();
        }

        public async Task<TeamMember?> GetTeamMemberByTeamIdAsync(Guid teamId, Guid userId)
        {
            var member = await _context.TeamMembers
                .Include(tm => tm.User)
                .Include(tm => tm.Team)
                .FirstOrDefaultAsync(x => x.TeamId == teamId && x.UserId == userId);
            
            return member;
        }

        public async Task AddTeamMemberAsync(TeamMember teamMember)
        {
            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTeamMemberAsync(TeamMember teamMember)
        {
            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamMemberAsync(TeamMember teamMember)
        {
            _context.TeamMembers.Update(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeamMember>> GetTeamMembersByTypeAsync(Guid teamId, MemberType memberType)
        {
            return await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId && tm.MemberType == memberType)
                .Include(tm => tm.User)
                .Include(tm => tm.Team)
                .ToListAsync();
        }
    }
}
