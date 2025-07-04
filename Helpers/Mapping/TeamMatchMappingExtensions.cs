using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMatches;

namespace Tournament.Management.API.Helpers.Mapping;

public static class TeamMatchMappingExtensions
{
    public static MatchDto ToDto(this TeamMatch match)
    {
        return new MatchDto(
            match.Id,
            new TeamSummaryDto(
                match.HomeTeamId,
                match.HomeTeam.Name,
                match.HomeTeam?.LogoUrl
            ),
            new TeamSummaryDto(
                match.AwayTeamId,
                match.AwayTeam.Name,
                match.AwayTeam?.LogoUrl
            ),
            match.HomeScore,
            match.AwayScore,
            match.MatchDate,
            match.Venue,
            match.Status
        );
    }

    public static MatchDetailDto ToDetailDto(this TeamMatch match)
    {
        return new MatchDetailDto(
            match.Id,
            new TeamSummaryDto(
                match.HomeTeamId,
                match.HomeTeam.Name,
                match.HomeTeam?.LogoUrl
            ),
            new TeamSummaryDto(
                match.AwayTeamId,
                match.AwayTeam.Name,
                match.AwayTeam?.LogoUrl
            ),
            match.HomeScore,
            match.AwayScore,
            match.MatchDate,
            match.Venue,
            match.Status,
            match.TournamentId,
            match.PlayerStats?.Select(ps => ps.ToDto())
        );
    }

    public static void UpdateFromDto(this TeamMatch match, MatchUpdateDto updateDto)
    {
        if (updateDto.HomeScore.HasValue) match.HomeScore = updateDto.HomeScore.Value;
        if (updateDto.AwayScore.HasValue) match.AwayScore = updateDto.AwayScore.Value;
        if (updateDto.MatchDate.HasValue) match.MatchDate = updateDto.MatchDate.Value;
        if (updateDto.Venue != null) match.Venue = updateDto.Venue;
        if (updateDto.Status.HasValue) match.Status = updateDto.Status.Value;
    }
}
