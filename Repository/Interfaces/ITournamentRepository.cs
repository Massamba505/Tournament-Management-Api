using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<UserTournament>> GetAllAsync();
        Task<UserTournament?> GetByIdAsync(Guid tournamentId);
        Task<UserTournament> CreateAsync(UserTournament tournament);
        Task<UserTournament?> UpdateAsync(Guid tournamentId, UserTournament tournament);
        Task<UserTournament?> DeleteAsync(Guid tournamentId);
    }
}
