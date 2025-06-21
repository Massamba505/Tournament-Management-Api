using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Helper;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Models.DTOs.TournamentFormat;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TournamentService(ITournamentRepository tournamentRepository, ITournamentFormatService tournamentFormatService) : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository = tournamentRepository;
        private readonly ITournamentFormatService _tournamentFormatService = tournamentFormatService;

        public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync()
        {
            var tournamentEntities = await _tournamentRepository.GetTournamentsAsync();
            return tournamentEntities.Select(DtoMapper.MapToTournamentDto);
        }

        public async Task<TournamentDto?> GetTournamentByIdAsync(Guid tournamentId)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);

            if (tournament is null)
            {
                return null;
            }

            return DtoMapper.MapToTournamentDto(tournament);
        }

        public async Task CreateTournamentAsync(CreateTournamentDto tournamentCreateDto)
        {
            var newTournament= new UserTournament
            {
                Name = tournamentCreateDto.Name,
                Description = tournamentCreateDto.Description,
                FormatId = tournamentCreateDto.FormatId,
                NumberOfTeams = tournamentCreateDto.NumberOfTeams,
                MaxPlayersPerTeam = tournamentCreateDto.MaxPlayersPerTeam,
                StartDate = tournamentCreateDto.StartDate,
                EndDate = tournamentCreateDto.EndDate,
                Location = tournamentCreateDto.Location,
                AllowJoinViaLink = tournamentCreateDto.AllowJoinViaLink,
                OrganizerId = tournamentCreateDto.OrganizerId,
                BannerImage = tournamentCreateDto.BannerImage,
                ContactEmail = tournamentCreateDto.ContactEmail,
                ContactPhone = tournamentCreateDto.ContactPhone,
                EntryFee = tournamentCreateDto.EntryFee,
                MatchDuration = tournamentCreateDto.MatchDuration,
                RegistrationDeadline = tournamentCreateDto.RegistrationDeadline,
                isPublic = tournamentCreateDto.IsPublic,
                CreatedAt = DateTime.Now
            };

            await _tournamentRepository.CreateTournamentAsync(newTournament);
        }

        public async Task<bool> UpdateTournamentAsync(Guid tournamentId, UpdateTournamentDto tournamentToUpdateDto)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);
            if (tournament is null)
            {
                return false;
            }

            var isUpdated = ApplyTournamentUpdates(tournament, tournamentToUpdateDto);
            if (!isUpdated)
            {
                return true;
            }

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

        public async Task<IEnumerable<TournamentFormatDto>> GetTournamentFormatsAsync()
        {
            var formats = await _tournamentFormatService.GetFormatsAsync();
            return formats;
        }

        private bool ApplyTournamentUpdates(UserTournament tournament, UpdateTournamentDto updated)
        {
            bool isUpdated = false;

            if (updated.Name != tournament.Name)
            {
                tournament.Name = updated.Name;
                isUpdated = true;
            }

            if (updated.Description != tournament.Description)
            {
                tournament.Description = updated.Description;
                isUpdated = true;
            }

            if (updated.FormatId != tournament.FormatId)
            {
                tournament.FormatId = updated.FormatId;
                isUpdated = true;
            }

            if (updated.NumberOfTeams != tournament.NumberOfTeams)
            {
                tournament.NumberOfTeams = updated.NumberOfTeams;
                isUpdated = true;
            }

            if (updated.MaxPlayersPerTeam != tournament.MaxPlayersPerTeam)
            {
                tournament.MaxPlayersPerTeam = updated.MaxPlayersPerTeam;
                isUpdated = true;
            }

            if (updated.StartDate != tournament.StartDate)
            {
                tournament.StartDate = updated.StartDate;
                isUpdated = true;
            }

            if (updated.EndDate != tournament.EndDate)
            {
                tournament.EndDate = updated.EndDate;
                isUpdated = true;
            }

            if (updated.Location != tournament.Location)
            {
                tournament.Location = updated.Location;
                isUpdated = true;
            }

            if (updated.AllowJoinViaLink != tournament.AllowJoinViaLink)
            {
                tournament.AllowJoinViaLink = updated.AllowJoinViaLink;
                isUpdated = true;
            }

            if (updated.BannerImage != tournament.BannerImage)
            {
                tournament.BannerImage = updated.BannerImage;
                isUpdated = true;
            }

            if (updated.ContactEmail != tournament.ContactEmail)
            {
                tournament.ContactEmail = updated.ContactEmail;
                isUpdated = true;
            }

            if (updated.ContactPhone != tournament.ContactPhone)
            {
                tournament.ContactPhone = updated.ContactPhone;
                isUpdated = true;
            }

            if (updated.EntryFee != tournament.EntryFee)
            {
                tournament.EntryFee = updated.EntryFee;
                isUpdated = true;
            }

            if (updated.MatchDuration != tournament.MatchDuration)
            {
                tournament.MatchDuration = updated.MatchDuration;
                isUpdated = true;
            }

            if (updated.RegistrationDeadline != tournament.RegistrationDeadline)
            {
                tournament.RegistrationDeadline = updated.RegistrationDeadline;
                isUpdated = true;
            }

            if (updated.IsPublic != tournament.isPublic)
            {
                tournament.isPublic = updated.IsPublic;
                isUpdated = true;
            }

            return isUpdated;
        }

        public async Task<IEnumerable<TournamentDto>> GetTournamentByOrganizerIdAsync(Guid userId)
        {
            var myTournament = await _tournamentRepository.GetTournamentByOrganizerIdAsync(userId);
            return myTournament.Select(DtoMapper.MapToTournamentDto);
        }
    }
}
