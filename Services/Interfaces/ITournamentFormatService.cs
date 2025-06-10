using Tournament.Management.API.Models.DTOs.TournamentFormat;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITournamentFormatService
    {
        Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync();
        Task<TournamentFormatDto?> GetFormatByIdAsync(int id);
    }
}
