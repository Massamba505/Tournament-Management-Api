using Tournament.Management.API.Models.DTOs.Teams;

namespace Tournament.Management.API.Models.DTOs.TournamentTeams;

public record TournamentTeamDto(
    Guid TeamId,
    string TeamName,
    string? LogoUrl,
    DateTime RegisteredAt
);

public record TournamentTeamDetailDto(
    TeamDto Team,
    DateTime RegisteredAt
);

public record JoinTournamentDto(
    Guid TeamId
);
