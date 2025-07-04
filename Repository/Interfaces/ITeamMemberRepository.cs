using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITeamMemberRepository
    {
        Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(Guid teamId);
        Task<TeamMember?> GetTeamMemberByTeamIdAsync(Guid teamId, Guid userId);
        Task<IEnumerable<TeamMember>> GetTeamMembersByTypeAsync(Guid teamId, MemberType memberType);
        Task AddTeamMemberAsync(TeamMember teamMember);
        Task RemoveTeamMemberAsync(TeamMember teamMember);
        Task UpdateTeamMemberAsync(TeamMember teamMember);
    }
}
