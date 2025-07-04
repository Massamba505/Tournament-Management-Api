using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.PlayerStats;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations
{
    public class PlayerStatService(
        IPlayerStatRepository playerStatRepository,
        ITeamMatchRepository teamMatchRepository,
        IUserRepository userRepository) : IPlayerStatService
    {
        private readonly IPlayerStatRepository _playerStatRepository = playerStatRepository;
        private readonly ITeamMatchRepository _teamMatchRepository = teamMatchRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<PlayerStatDto>> GetPlayerStatsByMatchIdAsync(Guid matchId)
        {
            var stats = await _playerStatRepository.GetPlayerStatsByMatchIdAsync(matchId);
            return stats.Select(s => s.ToDto());
        }

        public async Task<IEnumerable<PlayerStatDto>> GetPlayerStatsByPlayerIdAsync(Guid playerId)
        {
            var stats = await _playerStatRepository.GetPlayerStatsByPlayerIdAsync(playerId);
            return stats.Select(s => s.ToDto());
        }

        public async Task<PlayerStatDto?> GetPlayerStatByPlayerAndMatchAsync(Guid playerId, Guid matchId)
        {
            var stat = await _playerStatRepository.GetPlayerStatByPlayerAndMatchAsync(playerId, matchId);
            return stat?.ToDto();
        }

        public async Task CreatePlayerStatAsync(PlayerStatCreateDto createStat)
        {
            var player = await _userRepository.GetUserByIdAsync(createStat.PlayerId);
            if (player == null)
            {
                throw new ArgumentException("Player not found");
            }

            var match = await _teamMatchRepository.GetMatchByIdAsync(createStat.MatchId);
            if (match == null)
            {
                throw new ArgumentException("Match not found");
            }

            var playerStat = new PlayerStat
            {
                PlayerId = createStat.PlayerId,
                MatchId = createStat.MatchId,
                Goals = createStat.Goals,
                Assists = createStat.Assists,
                YellowCards = createStat.YellowCards,
                RedCards = createStat.RedCards,
                Position = createStat.Position
            };

            await _playerStatRepository.CreatePlayerStatAsync(playerStat);
        }

        public async Task<bool> UpdatePlayerStatAsync(Guid playerId, Guid matchId, PlayerStatUpdateDto updateStat)
        {
            var stat = await _playerStatRepository.GetPlayerStatByPlayerAndMatchAsync(playerId, matchId);
            if (stat == null)
            {
                return false;
            }

            stat.UpdateFromDto(updateStat);
            await _playerStatRepository.UpdatePlayerStatAsync(stat);
            return true;
        }

        public async Task<bool> DeletePlayerStatAsync(Guid playerId, Guid matchId)
        {
            var stat = await _playerStatRepository.GetPlayerStatByPlayerAndMatchAsync(playerId, matchId);
            if (stat == null)
            {
                return false;
            }

            await _playerStatRepository.DeletePlayerStatAsync(stat);
            return true;
        }
    }
}
