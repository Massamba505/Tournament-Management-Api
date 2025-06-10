using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Models.DTOs.TournamentFormat;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDto>> GetAllAsync();
        Task<TournamentDto?> GetByIdAsync(Guid tournamentId);
        Task<TournamentDto> CreateAsync(CreateTournamentDto tournamentCreateDto);
        Task<TournamentDto?> UpdateAsync(Guid tournamentId, UpdateTournamentDto tournamentToUpdateDto);
        Task<bool> DeleteAsync(Guid tournamentId);
        Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync();
    }
}
