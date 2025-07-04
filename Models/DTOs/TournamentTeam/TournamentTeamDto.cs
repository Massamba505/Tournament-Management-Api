using Tournament.Management.API.Models.DTOs.Teams;

namespace Tournament.Management.API.Models.DTOs.TournamentTeam;

public record TournamentTeamDto(
    TeamDto Team,
    DateTime RegisteredAt
);
