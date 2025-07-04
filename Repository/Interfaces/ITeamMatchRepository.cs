using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITeamMatchRepository
    {
        Task<IEnumerable<TeamMatch>> GetMatchesByTournamentIdAsync(Guid tournamentId);
        Task<TeamMatch?> GetMatchByIdAsync(Guid matchId);
        Task<IEnumerable<TeamMatch>> GetMatchesByTeamIdAsync(Guid teamId);
        Task<IEnumerable<TeamMatch>> GetMatchesByStatusAsync(Guid tournamentId, MatchStatus status);
        Task CreateMatchAsync(TeamMatch match);
        Task UpdateMatchAsync(TeamMatch match);
        Task DeleteMatchAsync(TeamMatch match);
        Task UpdateMatchStatusAsync(TeamMatch match, MatchStatus status);
    }
}
