using Tournament.Management.API.Models.DTOs.Team;
using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Models.DTOs.TournamentFormat;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDto>> GetAllAsync();
        Task<TournamentDto?> GetByIdAsync(Guid tournamentId);
        Task CreateAsync(CreateTournamentDto tournamentCreateDto);
        Task<bool> UpdateAsync(Guid tournamentId, UpdateTournamentDto tournamentToUpdateDto);
        Task<bool> DeleteAsync(Guid tournamentId);
        Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync();
        Task<IEnumerable<TournamentDto>> GetTournamentByOrganizerAsync(Guid userId);
    }
}
