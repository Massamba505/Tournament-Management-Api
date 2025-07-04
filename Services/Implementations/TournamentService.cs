using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations
{
    public class TournamentService(ITournamentRepository tournamentRepository, ITournamentFormatService tournamentFormatService) : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository = tournamentRepository;
        private readonly ITournamentFormatService _tournamentFormatService = tournamentFormatService;

        public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync()
        {
            var tournamentEntities = await _tournamentRepository.GetTournamentsAsync();
            return tournamentEntities.Select(t => t.ToDto());
        }

        public async Task<TournamentDto?> GetTournamentByIdAsync(Guid tournamentId)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);
            if(tournament is null)
            {
                return null;
            }

            return tournament.ToDto();
        }

        public async Task<TournamentDetailDto?> GetTournamentDetailsByIdAsync(Guid id)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(id);
            if (tournament is null)
            {
                return null;
            }

            return tournament.ToDetailDto();
        }

        public async Task CreateTournamentAsync(TournamentCreateDto tournamentCreateDto)
        {
            var newTournament = tournamentCreateDto.ToEntity();
            await _tournamentRepository.CreateTournamentAsync(newTournament);
        }

        public async Task<bool> UpdateTournamentAsync(Guid tournamentId, TournamentUpdateDto tournamentUpdateDto)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);
            if (tournament is null)
            {
                return false;
            }

            tournament.UpdateFromDto(tournamentUpdateDto);
            await _tournamentRepository.UpdateTournamentAsync(tournament);
            return true;
        }

        public async Task<bool> DeleteTournamentAsync(Guid tournamentId)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);
            if (tournament is null)
            {
                return false;
            }

            await _tournamentRepository.DeleteTournamentAsync(tournament);
            return true;
        }

        public async Task<IEnumerable<TournamentFormatEnum>> GetTournamentFormatsAsync()
        {
            return await _tournamentFormatService.GetFormatsAsync();
        }

        public async Task<IEnumerable<TournamentDto>> GetTournamentsByOrganizerIdAsync(Guid userId)
        {
            var myTournaments = await _tournamentRepository.GetTournamentByOrganizerIdAsync(userId);
            return myTournaments.Select(t => t.ToDto());
        }

        public async Task<IEnumerable<TournamentDto>> GetTournamentsByStatusAsync(TournamentStatus status)
        {
            var tournaments = await _tournamentRepository.GetTournamentsByStatusAsync(status);
            return tournaments.Select(t => t.ToDto());
        }

        public async Task<bool> UpdateTournamentStatusAsync(Guid id, TournamentStatus status)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(id);
            if (tournament is null)
            {
                return false;
            }

            tournament.Status = status;
            await _tournamentRepository.UpdateTournamentAsync(tournament);
            return true;
        }
    }
}
