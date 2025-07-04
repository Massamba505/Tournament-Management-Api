using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeamsByUserIdAsync(Guid userId);
        Task<Team?> GetTeamByIdAsync(Guid id);
        Task CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(Team team);
        Task UpdateTeamStatusAsync(Team team, TeamStatus status);
        Task<IEnumerable<Team>> GetTeamsByStatusAsync(TeamStatus status);
    }
}
