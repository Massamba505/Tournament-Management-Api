using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetMyTeamsAsync(Guid userId);
        Task<Team?> GetByIdAsync(Guid id, Guid userId);
        Task CreateAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(Team team);
        Task DeactivateAsync(Team team);
        Task ActivateAsync(Team team);
    }
}
