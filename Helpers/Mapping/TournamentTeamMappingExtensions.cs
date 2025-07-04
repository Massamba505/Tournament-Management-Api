using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentTeams;

namespace Tournament.Management.API.Helpers.Mapping;

public static class TournamentTeamMappingExtensions
{
    public static TournamentTeamDto ToDto(this TournamentTeam team)
    {
        return new TournamentTeamDto(
            team.TeamId,
            team.Team?.Name ?? string.Empty,
            team.Team?.LogoUrl,
            team.RegisteredAt
        );
    }
    
    public static TournamentTeamDetailDto ToDetailDto(this TournamentTeam team)
    {
        return new TournamentTeamDetailDto(
            TeamMappingExtensions.ToDto(team.Team),
            team.RegisteredAt
        );
    }
}
