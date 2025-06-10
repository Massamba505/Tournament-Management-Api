using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentFormat;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITournamentFormatRepository
    {
        Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync();
        Task<TournamentFormatDto?> GetByIdAsync(int id);
        
    }
}
