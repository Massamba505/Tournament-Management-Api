using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TeamMatches;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations;

public class TeamMatchService(
    ITeamMatchRepository teamMatchRepository,
    ITournamentRepository tournamentRepository,
    ITeamRepository teamRepository) : ITeamMatchService
{
    private readonly ITeamMatchRepository _teamMatchRepository = teamMatchRepository;
    private readonly ITournamentRepository _tournamentRepository = tournamentRepository;
    private readonly ITeamRepository _teamRepository = teamRepository;

    public async Task<IEnumerable<MatchDto>> GetMatchesByTournamentIdAsync(Guid tournamentId)
    {
        var matches = await _teamMatchRepository.GetMatchesByTournamentIdAsync(tournamentId);
        return matches.Select(m => m.ToDto());
    }

    public async Task<MatchDetailDto?> GetMatchByIdAsync(Guid matchId)
    {
        var match = await _teamMatchRepository.GetMatchByIdAsync(matchId);
        return match?.ToDetailDto();
    }

    public async Task<IEnumerable<MatchDto>> GetMatchesByTeamIdAsync(Guid teamId)
    {
        var matches = await _teamMatchRepository.GetMatchesByTeamIdAsync(teamId);
        return matches.Select(m => m.ToDto());
    }

    public async Task<IEnumerable<MatchDto>> GetMatchesByStatusAsync(Guid tournamentId, MatchStatus status)
    {
        var matches = await _teamMatchRepository.GetMatchesByStatusAsync(tournamentId, status);
        return matches.Select(m => m.ToDto());
    }

    public async Task CreateMatchAsync(MatchCreateDto createMatch)
    {
        var tournament = await _tournamentRepository.GetTournamentByIdAsync(createMatch.TournamentId);
        if (tournament is null)
        {
            throw new ArgumentException("Tournament not found");
        }

        var homeTeam = await _teamRepository.GetTeamByIdAsync(createMatch.HomeTeamId);
        if (homeTeam is null)
        {
            throw new ArgumentException("Home team not found");
        }

        var awayTeam = await _teamRepository.GetTeamByIdAsync(createMatch.AwayTeamId);
        if (awayTeam is null)
        {
            throw new ArgumentException("Away team not found");
        }

        var match = new TeamMatch
        {
            Id = Guid.NewGuid(),
            TournamentId = createMatch.TournamentId,
            HomeTeamId = createMatch.HomeTeamId,
            AwayTeamId = createMatch.AwayTeamId,
            MatchDate = createMatch.MatchDate,
            Venue = createMatch.Venue,
            Status = createMatch.Status,
            HomeScore = 0,
            AwayScore = 0
        };

        await _teamMatchRepository.CreateMatchAsync(match);
    }

    public async Task<bool> UpdateMatchAsync(Guid matchId, MatchUpdateDto updateMatch)
    {
        var match = await _teamMatchRepository.GetMatchByIdAsync(matchId);
        if (match is null)
        {
            return false;
        }

        match.UpdateFromDto(updateMatch);
        await _teamMatchRepository.UpdateMatchAsync(match);
        return true;
    }

    public async Task<bool> DeleteMatchAsync(Guid matchId)
    {
        var match = await _teamMatchRepository.GetMatchByIdAsync(matchId);
        if (match is null)
        {
            return false;
        }

        await _teamMatchRepository.DeleteMatchAsync(match);
        return true;
    }

    public async Task<bool> UpdateMatchStatusAsync(Guid matchId, MatchStatus status)
    {
        var match = await _teamMatchRepository.GetMatchByIdAsync(matchId);
        if (match is null)
        {
            return false;
        }

        await _teamMatchRepository.UpdateMatchStatusAsync(match, status);
        return true;
    }
}
