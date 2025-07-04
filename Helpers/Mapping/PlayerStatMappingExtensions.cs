using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.PlayerStats;

namespace Tournament.Management.API.Helpers.Mapping;

public static class PlayerStatMappingExtensions
{
    public static PlayerStatDto ToDto(this PlayerStat playerStat)
    {
        return new PlayerStatDto(
            playerStat.PlayerId,
            playerStat.MatchId,
            $"{playerStat.Player.Name} {playerStat.Player.Surname}",
            playerStat.Goals,
            playerStat.Assists,
            playerStat.YellowCards,
            playerStat.RedCards,
            playerStat.Position
        );
    }
    
    public static PlayerStat ToEntity(this PlayerStatCreateDto dto)
    {
        return new PlayerStat
        {
            PlayerId = dto.PlayerId,
            MatchId = dto.MatchId,
            Goals = dto.Goals,
            Assists = dto.Assists,
            YellowCards = dto.YellowCards,
            RedCards = dto.RedCards,
            Position = dto.Position
        };
    }
    
    public static void UpdateFromDto(this PlayerStat playerStat, PlayerStatUpdateDto dto)
    {
        if (dto.Goals.HasValue)
            playerStat.Goals = dto.Goals.Value;
            
        if (dto.Assists.HasValue)
            playerStat.Assists = dto.Assists.Value;
            
        if (dto.YellowCards.HasValue)
            playerStat.YellowCards = dto.YellowCards.Value;
            
        if (dto.RedCards.HasValue)
            playerStat.RedCards = dto.RedCards.Value;
            
        if (dto.Position.HasValue)
            playerStat.Position = dto.Position.Value;
    }
}
