using Tournament.Management.API.Models.DTOs.TeamMatches;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces;

public interface ITeamMatchService
{
    Task<IEnumerable<MatchDto>> GetMatchesByTournamentIdAsync(Guid tournamentId);
    Task<MatchDetailDto?> GetMatchByIdAsync(Guid matchId);
    Task<IEnumerable<MatchDto>> GetMatchesByTeamIdAsync(Guid teamId);
    Task<IEnumerable<MatchDto>> GetMatchesByStatusAsync(Guid tournamentId, MatchStatus status);
    Task CreateMatchAsync(MatchCreateDto createMatch);
    Task<bool> UpdateMatchAsync(Guid matchId, MatchUpdateDto updateMatch);
    Task<bool> DeleteMatchAsync(Guid matchId);
    Task<bool> UpdateMatchStatusAsync(Guid matchId, MatchStatus status);
}
