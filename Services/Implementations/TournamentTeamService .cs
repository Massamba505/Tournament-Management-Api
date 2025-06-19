using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentTeam;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TournamentTeamService(ITournamentTeamRepository tournamentTeamRepository) : ITournamentTeamService
    {
        private readonly ITournamentTeamRepository _tournamentTeamRepository = tournamentTeamRepository;

        public async Task AddTournamentTeamAsync(Guid tournamentId, JoinTournamentDto join)
        {
            var tournamentTeam = new TournamentTeam
            {
                Id = Guid.NewGuid(),
                TournamentId = tournamentId,
                TeamId = join.TeamId,
                RegisteredAt = DateTime.Now,
                IsActive = true
            };

            await _tournamentTeamRepository.AddTournamentTeamAsync(tournamentTeam);
        }

        public async Task<TournamentTeamDto?> GetTournamentTeamByTeamIdAsync(Guid tournamentId, Guid id)
        {
            var tournamentTeam = await _tournamentTeamRepository.GetTournamentTeamByTeamIdAsync(tournamentId, id);
            if(tournamentTeam is null)
            {
                return null;
            }

            return MapToDto(tournamentTeam);
        }

        public async Task<IEnumerable<TournamentTeamDto>> GetTournamentTeamsByTournamentIdAsync(Guid tournamentId)
        {
            var entities = await _tournamentTeamRepository.GetTournamentTeamsByTournamentIdAsync(tournamentId);
            return entities.Select(MapToDto);
        }

        public async Task<bool> RemoveTournamentTeamAsync(Guid tournamentId, Guid id)
        {
            var tournamentTeam = await _tournamentTeamRepository.GetTournamentTeamByTeamIdAsync(tournamentId, id);
            if (tournamentTeam is null)
            {
                return false;
            }

            await _tournamentTeamRepository.DeleteTournamentTeamAsync(tournamentTeam);
            return true;
        }

        private static TournamentTeamDto MapToDto(TournamentTeam tournamentTeam)
        {
            return new(
                tournamentTeam.Id,
                tournamentTeam.TeamId,
                tournamentTeam.Team.Name,
                tournamentTeam.Team.LogoUrl,
                tournamentTeam.RegisteredAt,
                tournamentTeam.IsActive
            );
        }

    }
}
