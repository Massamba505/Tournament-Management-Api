using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Repository.Interfaces;

public interface ITournamentFormatRepository
{
    Task<IEnumerable<TournamentFormatEnum>> GetFormatsAsync();
    Task<string> GetFormatNameAsync(TournamentFormatEnum format);
    Task<bool> IsValidFormatAsync(TournamentFormatEnum format);
}
