using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces;

public interface ITournamentFormatService
{
    Task<IEnumerable<TournamentFormatEnum>> GetFormatsAsync();
    Task<string> GetFormatNameAsync(TournamentFormatEnum format);
    Task<bool> IsValidFormatAsync(TournamentFormatEnum format);
}
