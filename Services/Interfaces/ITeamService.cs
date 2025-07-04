using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetMyTeamsAsync(Guid userId);
        Task<TeamDto?> GetTeamByIdAsync(Guid id);
        Task<TeamDetailDto?> GetTeamDetailsByIdAsync(Guid id);
        Task CreateTeamAsync(Guid managerId, TeamCreateDto dto);
        Task<bool> UpdateTeamAsync(Guid id, TeamUpdateDto dto);
        Task<bool> DeleteTeamAsync(Guid id);
        Task<bool> UpdateTeamStatusAsync(Guid id, TeamStatus status);
        Task<IEnumerable<TeamDto>> GetTeamsByStatusAsync(TeamStatus status);
    }
}
