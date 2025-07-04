using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TeamMembers;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations
{
    public class TeamMemberService(ITeamMemberRepository teamMemberRepository, ITeamRepository teamRepository) : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;

        public async Task<IEnumerable<TeamMemberDto>> GetTeamMembersAsync(Guid teamId)
        {
            var members = await _teamMemberRepository.GetTeamMembersByTeamIdAsync(teamId);
            return members.Select(m => m.ToDto());
        }

        public async Task<IEnumerable<TeamMemberDto>> GetTeamMembersByTypeAsync(Guid teamId, MemberType memberType)
        {
            var members = await _teamMemberRepository.GetTeamMembersByTypeAsync(teamId, memberType);
            return members.Select(m => m.ToDto());
        }

        public async Task AddTeamMemberAsync(Guid teamId, AddTeamMemberDto newTeamMember)
        {
            var teamMember = new TeamMember
            {
                TeamId = teamId,
                UserId = newTeamMember.UserId,
                MemberType = MemberType.Player,
                JoinedAt = DateTime.UtcNow
            };

            await _teamMemberRepository.AddTeamMemberAsync(teamMember);
        }

        public async Task<bool> RemoveTeamMemberAsync(Guid teamId, Guid userId)
        {
            var teamMember = await _teamMemberRepository.GetTeamMemberByTeamIdAsync(teamId, userId);
            if(teamMember is null)
            {
                return false;
            }

            // If this user is the captain, remove the captain reference from the team
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (team != null && team.CaptainId == userId)
            {
                team.CaptainId = null;
                await _teamRepository.UpdateTeamAsync(team);
            }

            await _teamMemberRepository.RemoveTeamMemberAsync(teamMember);
            return true;
        }

        public async Task<bool> AssignTeamCaptainAsync(Guid teamId, Guid userId)
        {
            // Simply use the UpdateMemberTypeAsync method with MemberType.Captain
            return await UpdateMemberTypeAsync(teamId, userId, MemberType.Captain);
        }

        public async Task<bool> UpdateMemberTypeAsync(Guid teamId, Guid userId, MemberType memberType)
        {
            var teamMember = await _teamMemberRepository.GetTeamMemberByTeamIdAsync(teamId, userId);
            if (teamMember is null)
            {
                return false;
            }

            var previousMemberType = teamMember.MemberType;
            teamMember.MemberType = memberType;
            await _teamMemberRepository.UpdateTeamMemberAsync(teamMember);
            
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (team is null)
            {
                return true; // Member type was updated, but team not found
            }
            
            // If changing to Captain, also update the team's captain reference
            if (memberType == MemberType.Captain)
            {
                // If there's already a captain, demote them first
                if (team.CaptainId.HasValue && team.CaptainId.Value != userId)
                {
                    var currentCaptain = await _teamMemberRepository.GetTeamMemberByTeamIdAsync(teamId, team.CaptainId.Value);
                    if (currentCaptain != null && currentCaptain.MemberType == MemberType.Captain)
                    {
                        currentCaptain.MemberType = MemberType.Player;
                        await _teamMemberRepository.UpdateTeamMemberAsync(currentCaptain);
                    }
                }
                
                team.CaptainId = userId;
                await _teamRepository.UpdateTeamAsync(team);
            }
            // If this user was a captain and is now being changed to something else, remove captain reference
            else if (previousMemberType == MemberType.Captain && team.CaptainId == userId)
            {
                team.CaptainId = null;
                await _teamRepository.UpdateTeamAsync(team);
            }
            
            return true;
        }
    }
}
