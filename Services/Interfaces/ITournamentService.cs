using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Models.DTOs.TournamentFormat;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDto>> GetTournamentsAsync();
        Task<TournamentDto?> GetTournamentByIdAsync(Guid Id);
        Task CreateTournamentAsync(CreateTournamentDto dto);
        Task<bool> UpdateTournamentAsync(Guid id, UpdateTournamentDto tournament);
        Task<bool> DeleteTournamentAsync(Guid id);
        Task<IEnumerable<TournamentDto>> GetTournamentByOrganizerIdAsync(Guid userId);
        Task<IEnumerable<TournamentFormatDto>> GetTournamentFormatsAsync();
    }

}
