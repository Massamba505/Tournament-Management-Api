﻿using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain;

public class PlayerStat
{
    public Guid PlayerId { get; set; }
    public Guid MatchId { get; set; }
    
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int YellowCards { get; set; }
    public int RedCards { get; set; }
    public PlayerPosition Position { get; set; } = PlayerPosition.Forward;

    public User Player { get; set; } = null!;
    public TeamMatch Match { get; set; } = null!;
}
