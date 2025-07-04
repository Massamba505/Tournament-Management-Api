using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces;

public interface ITournamentService
{
    Task<IEnumerable<TournamentDto>> GetTournamentsAsync();
    Task<TournamentDto?> GetTournamentByIdAsync(Guid Id);
    Task<TournamentDetailDto?> GetTournamentDetailsByIdAsync(Guid id);
    Task CreateTournamentAsync(TournamentCreateDto dto);
    Task<bool> UpdateTournamentAsync(Guid id, TournamentUpdateDto tournament);
    Task<bool> DeleteTournamentAsync(Guid id);
    Task<IEnumerable<TournamentDto>> GetTournamentsByOrganizerIdAsync(Guid userId);
    Task<IEnumerable<TournamentDto>> GetTournamentsByStatusAsync(TournamentStatus status);
    Task<bool> UpdateTournamentStatusAsync(Guid id, TournamentStatus status);
}
