using Tournament.Management.API.Models.DTOs.Team;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetMyTeamsAsync(Guid userId);
        Task<TeamDto?> GetTeamByIdAsync(Guid id);
        Task CreateTeamAsync(Guid managerId, TeamCreateDto dto);
        Task<bool> UpdateTeamAsync(Guid id, TeamUpdateDto dto);
        Task<bool> DeleteTeamAsync(Guid id);
        Task<bool> DeactivateTeamAsync(Guid id);
        Task<bool> ActivateTeamAsync(Guid id);
    }
}
