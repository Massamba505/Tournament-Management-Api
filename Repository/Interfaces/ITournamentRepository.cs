using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Repository.Interfaces;

public interface ITournamentRepository
{
    Task<IEnumerable<UserTournament>> GetTournamentsAsync();
    Task<UserTournament?> GetTournamentByIdAsync(Guid Id);
    Task CreateTournamentAsync(UserTournament tournament);
    Task UpdateTournamentAsync(UserTournament tournament);
    Task DeleteTournamentAsync(UserTournament tournament);
    Task<IEnumerable<UserTournament>> GetTournamentByOrganizerIdAsync(Guid userId);
    Task<IEnumerable<UserTournament>> GetTournamentsByStatusAsync(TournamentStatus status);
}
