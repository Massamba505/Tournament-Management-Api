﻿using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class TournamentFormatRepository : ITournamentFormatRepository
{

    public Task<IEnumerable<TournamentFormatEnum>> GetFormatsAsync()
    {
        return Task.FromResult<IEnumerable<TournamentFormatEnum>>(Enum.GetValues<TournamentFormatEnum>());
    }

    public Task<string> GetFormatNameAsync(TournamentFormatEnum format)
    {
        return Task.FromResult(format.ToString());
    }

    public Task<bool> IsValidFormatAsync(TournamentFormatEnum format)
    {
        bool isValid = Enum.IsDefined(format);
        return Task.FromResult(isValid);
    }
}
