using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<UserTournament>> GetAllAsync();
        Task<UserTournament?> GetByIdAsync(Guid tournamentId);
        Task CreateAsync(UserTournament tournament);
        Task UpdateAsync(UserTournament tournament);
        Task DeleteAsync(UserTournament tournamentId);
        Task<IEnumerable<UserTournament>> GetTournamentByOrganizerAsync(Guid userId);
    }
}
