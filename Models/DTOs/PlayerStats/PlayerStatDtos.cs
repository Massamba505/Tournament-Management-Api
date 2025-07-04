using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.PlayerStats;

public record PlayerStatDto(
    Guid PlayerId,
    Guid MatchId,
    string PlayerName,
    int Goals,
    int Assists,
    int YellowCards,
    int RedCards,
    PlayerPosition Position
);

public record PlayerStatCreateDto(
    Guid PlayerId,
    Guid MatchId,
    int Goals,
    int Assists,
    int YellowCards,
    int RedCards,
    PlayerPosition Position
);

public record PlayerStatUpdateDto(
    int? Goals,
    int? Assists,
    int? YellowCards,
    int? RedCards,
    PlayerPosition? Position
);
