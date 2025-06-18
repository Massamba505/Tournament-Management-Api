using Tournament.Management.API.Models.DTOs.TeamMember;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITeamMemberRepository
    {
        Task<IEnumerable<TeamMemberDto>> GetMembersByTeamIdAsync(Guid teamId);
        Task AddMemberAsync(Guid teamId, AddTeamMemberDto dto);
        Task RemoveMemberAsync(Guid teamId, Guid userId);
        Task AssignCaptainAsync(Guid teamId, Guid userId);
        Task UnassignCaptainAsync(Guid teamId, Guid userId);
    }

}
