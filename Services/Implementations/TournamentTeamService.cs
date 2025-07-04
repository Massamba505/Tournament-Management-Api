using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations
{
    public class TournamentTeamService(ITournamentTeamRepository tournamentTeamRepository, ITeamRepository teamRepository) : ITournamentTeamService
    {
        private readonly ITournamentTeamRepository _tournamentTeamRepository = tournamentTeamRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;

        public async Task<IEnumerable<TournamentTeamDto>> GetTournamentTeamsByTournamentIdAsync(Guid tournamentId)
        {
            var tournamentTeams = await _tournamentTeamRepository.GetTournamentTeamsByTournamentIdAsync(tournamentId);
            return tournamentTeams.Select(tt => tt.ToDto());
        }

        public async Task<TournamentTeamDto?> GetTournamentTeamByTeamIdAsync(Guid tournamentId, Guid teamId)
        {
            var tournamentTeam = await _tournamentTeamRepository.GetTournamentTeamByTeamIdAsync(tournamentId, teamId);
            return tournamentTeam?.ToDto();
        }

        public async Task<TournamentTeamDetailDto?> GetTournamentTeamDetailsByTeamIdAsync(Guid tournamentId, Guid teamId)
        {
            var tournamentTeam = await _tournamentTeamRepository.GetTournamentTeamByTeamIdAsync(tournamentId, teamId);
            return tournamentTeam?.ToDetailDto();
        }

        public async Task AddTournamentTeamAsync(Guid tournamentId, JoinTournamentDto join)
        {
            var team = await _teamRepository.GetTeamByIdAsync(join.TeamId);
            if (team == null)
            {
                throw new ArgumentException("Team not found");
            }

            // Check if the team is already in the tournament
            var existingEntry = await _tournamentTeamRepository.GetTournamentTeamByTeamIdAsync(tournamentId, join.TeamId);
            if (existingEntry != null)
            {
                throw new InvalidOperationException("Team is already registered for this tournament");
            }

            var tournamentTeam = new TournamentTeam
            {
                TournamentId = tournamentId,
                TeamId = join.TeamId,
                RegisteredAt = DateTime.UtcNow
            };

            await _tournamentTeamRepository.AddTournamentTeamAsync(tournamentTeam);
        }

        public async Task<bool> RemoveTournamentTeamAsync(Guid tournamentId, Guid teamId)
        {
            var tournamentTeam = await _tournamentTeamRepository.GetTournamentTeamByTeamIdAsync(tournamentId, teamId);
            if (tournamentTeam == null)
            {
                return false;
            }

            await _tournamentTeamRepository.DeleteTournamentTeamAsync(tournamentTeam);
            return true;
        }
    }
}
