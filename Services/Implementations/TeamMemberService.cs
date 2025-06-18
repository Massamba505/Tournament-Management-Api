using Tournament.Management.API.Models.DTOs.TeamMember;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TeamMemberService(ITeamMemberRepository teamMemberRepository) : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;

        public async Task<IEnumerable<TeamMemberDto>> GetMembersAsync(Guid teamId)
        {
            var members = await _teamMemberRepository.GetMembersByTeamIdAsync(teamId);
            return members;
        }

        public async Task AddMemberAsync(Guid teamId, AddTeamMemberDto dto)
        {
            await _teamMemberRepository.AddMemberAsync(teamId, dto);
        }

        public async Task RemoveMemberAsync(Guid teamId, Guid userId)
        {
            await _teamMemberRepository.RemoveMemberAsync(teamId, userId);
        }

        public async Task AssignCaptainAsync(Guid teamId, Guid userId)
        {
            await _teamMemberRepository.AssignCaptainAsync(teamId, userId);
        }

        public async Task UnassignCaptainAsync(Guid teamId, Guid userId)
        {
            await _teamMemberRepository.UnassignCaptainAsync(teamId,userId);
        }
    }

}
