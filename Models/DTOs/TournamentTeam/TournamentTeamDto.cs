using Tournament.Management.API.Models.DTOs.Team;

namespace Tournament.Management.API.Models.DTOs.TournamentTeam
{
    public record TournamentTeamDto(
        TeamDto Team,
        DateTime RegisteredAt
    );
}
