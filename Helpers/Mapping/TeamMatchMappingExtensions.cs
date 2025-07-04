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
}
