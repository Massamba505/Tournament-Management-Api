using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces;

public interface ITournamentRepository
{
    Task<IEnumerable<UserTournament>> GetTournamentsAsync();
    Task<UserTournament?> GetTournamentByIdAsync(Guid Id);
    Task CreateTournamentAsync(UserTournament tournament);
    Task UpdateTournamentAsync(UserTournament tournament);
    Task DeleteTournamentAsync(UserTournament tournament);
    Task<IEnumerable<UserTournament>> GetTournamentByOrganizerIdAsync(Guid userId);
}
