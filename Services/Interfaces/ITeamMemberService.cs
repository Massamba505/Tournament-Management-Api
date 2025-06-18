using Tournament.Management.API.Models.DTOs.TeamMember;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMemberDto>> GetMembersAsync(Guid teamId);
        Task AddMemberAsync(Guid teamId, AddTeamMemberDto dto);
        Task RemoveMemberAsync(Guid teamId, Guid userId);
        Task AssignCaptainAsync(Guid teamId, Guid userId);
        Task UnassignCaptainAsync(Guid teamId, Guid userId);
    }

}
