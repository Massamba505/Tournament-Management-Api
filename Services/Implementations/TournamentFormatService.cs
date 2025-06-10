using Tournament.Management.API.Models.DTOs.TournamentFormat;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TournamentFormatService(ITournamentFormatRepository tournamentFormatRepository) : ITournamentFormatService
    {
        private readonly ITournamentFormatRepository _tournamentFormatRepository = tournamentFormatRepository;

        public async Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync()
        {
            return await _tournamentFormatRepository.GetFormatsAsync();
        }

        public async Task<TournamentFormatDto?> GetFormatByIdAsync(int id)
        {
            return await _tournamentFormatRepository.GetByIdAsync(id);
        }
    }
}
