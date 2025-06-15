using Tournament.Management.API.Models.DTOs.Team;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetMyTeamsAsync(Guid userId);
        Task<TeamDto?> GetByIdAsync(Guid id, Guid userId);
        Task CreateAsync(Guid managerId, TeamCreateDto dto);
        Task<bool> UpdateAsync(Guid id, TeamUpdateDto dto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<bool> DeactivateAsync(Guid id, Guid userId);
        Task<bool> ActivateAsync(Guid id, Guid userId);
    }
}
