using Tournament.Management.API.Models.DTOs.TeamMember;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMemberDto>> GetTeamMembersAsync(Guid teamId);
        Task AddTeamMemberAsync(Guid teamId, AddTeamMemberDto dto);
        Task<bool> RemoveTeamMemberAsync(Guid teamId, Guid userId);
        Task<bool> AssignTeamCaptainAsync(Guid teamId, Guid userId);
        Task<bool> UnassignTeamCaptainAsync(Guid teamId, Guid userId);
    }

}
