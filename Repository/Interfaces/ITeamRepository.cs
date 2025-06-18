using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeamsByUserIdAsync(Guid userId);
        Task<Team?> GetTeamByIdAsync(Guid id);
        Task CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(Team team);
        Task DeactivateTeamAsync(Team team);
        Task ActivateTeamAsync(Team team);
    }
}
