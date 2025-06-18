using Microsoft.EntityFrameworkCore;
using System;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TeamMember;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations
{
    public class TeamMemberRepository(TournamentManagerContext context) : ITeamMemberRepository
    {
        private readonly TournamentManagerContext _context = context;

        public async Task<IEnumerable<TeamMemberDto>> GetMembersByTeamIdAsync(Guid teamId)
        {
            return await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Include(tm => tm.User)
                .Include(tm => tm.Member)
                .Select(tm => new TeamMemberDto(
                    tm.UserId,
                    $"{tm.User.Name} {tm.User.Surname}",
                    tm.User.Email,
                    tm.Member.Name,
                    tm.IsCaptain,
                    tm.JoinedAt
                ))
                .ToListAsync();
        }

        public async Task AddMemberAsync(Guid teamId, AddTeamMemberDto dto)
        {
            var teamMember = new TeamMember
            {
                Id = Guid.NewGuid(),
                TeamId = teamId,
                UserId = dto.UserId,
                MemberId = dto.MemberId,
                IsCaptain = false
            };

            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMemberAsync(Guid teamId, Guid userId)
        {
            var member = await _context.TeamMembers
                .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.UserId == userId);

            if (member != null)
            {
                _context.TeamMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignCaptainAsync(Guid teamId, Guid userId)
        {
            var members = _context.TeamMembers
                .Where(tm => tm.TeamId == teamId);

            foreach (var member in members)
            {
                member.IsCaptain = member.UserId == userId;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UnassignCaptainAsync(Guid teamId, Guid userId)
        {
            var members = await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId && tm.UserId == userId && tm.IsCaptain)
                .ToListAsync();

            foreach (var member in members)
            {
                member.IsCaptain = false;
            }

            await _context.SaveChangesAsync();
        }

    }

}
