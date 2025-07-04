using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Tournaments;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TeamMatchMappingExtensions
    {
        public static MatchDto ToDto(this TeamMatch match)
        {
            return new MatchDto
            {
                Id = match.Id,
                HomeTeam = new TeamSummaryDto
                {
                    Id = match.HomeTeamId,
                    Name = match.HomeTeam?.Name ?? string.Empty,
                    LogoUrl = match.HomeTeam?.LogoUrl
                },
                AwayTeam = new TeamSummaryDto
                {
                    Id = match.AwayTeamId,
                    Name = match.AwayTeam?.Name ?? string.Empty,
                    LogoUrl = match.AwayTeam?.LogoUrl
                },
                HomeScore = match.HomeScore,
                AwayScore = match.AwayScore,
                MatchDate = match.MatchDate,
                Venue = match.Venue,
                Status = match.Status,
                Round = match.Round
            };
        }
    }
}
