using Tournament.Management.API.Models.DTOs.TeamMembers;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMemberDto>> GetTeamMembersAsync(Guid teamId);
        Task<IEnumerable<TeamMemberDto>> GetTeamMembersByTypeAsync(Guid teamId, MemberType memberType);
        Task AddTeamMemberAsync(Guid teamId, AddTeamMemberDto dto);
        Task<bool> RemoveTeamMemberAsync(Guid teamId, Guid userId);
        Task<bool> AssignTeamCaptainAsync(Guid teamId, Guid userId);
        Task<bool> UpdateMemberTypeAsync(Guid teamId, Guid userId, MemberType memberType);
    }
}
