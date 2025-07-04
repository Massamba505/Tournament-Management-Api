using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.PlayerStats;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.TeamMatches;

public record MatchDto(
    Guid Id,
    TeamSummaryDto HomeTeam,
    TeamSummaryDto AwayTeam,
    int? HomeScore,
    int? AwayScore,
    DateTime MatchDate,
    string Venue,
    MatchStatus Status
);

public record MatchDetailDto(
    Guid Id,
    TeamSummaryDto HomeTeam,
    TeamSummaryDto AwayTeam,
    int? HomeScore,
    int? AwayScore,
    DateTime MatchDate,
    string Venue,
    MatchStatus Status,
    Guid TournamentId,
    IEnumerable<PlayerStatDto>? PlayerStats
);

public record MatchCreateDto(
    Guid TournamentId,
    Guid HomeTeamId,
    Guid AwayTeamId,
    DateTime MatchDate,
    string Venue,
    MatchStatus Status = MatchStatus.Scheduled
);

public record MatchUpdateDto(
    int? HomeScore,
    int? AwayScore,
    DateTime? MatchDate,
    string? Venue,
    MatchStatus? Status
);
