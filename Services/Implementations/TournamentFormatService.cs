using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TournamentFormatService(ITournamentFormatRepository tournamentFormatRepository) : ITournamentFormatService
    {
        private readonly ITournamentFormatRepository _tournamentFormatRepository = tournamentFormatRepository;

        public async Task<IEnumerable<TournamentFormatEnum>> GetFormatsAsync()
        {
            return await _tournamentFormatRepository.GetFormatsAsync();
        }

        public async Task<string> GetFormatNameAsync(TournamentFormatEnum format)
        {
            return await _tournamentFormatRepository.GetFormatNameAsync(format);
        }

        public async Task<bool> IsValidFormatAsync(TournamentFormatEnum format)
        {
            return await _tournamentFormatRepository.IsValidFormatAsync(format);
        }
    }
}
