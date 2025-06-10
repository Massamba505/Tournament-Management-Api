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

        public async Task<IEnumerable<TournamentDto>> GetAllAsync()
        {
            var tournamentEntities = await _tournamentRepository.GetAllAsync();

            return tournamentEntities.Select(MapToTournamentDto);
        }

        public async Task<TournamentDto?> GetByIdAsync(Guid tournamentId)
        {
            var tournament= await _tournamentRepository.GetByIdAsync(tournamentId);

            if (tournament == null)
            {
                return null;
            }


            return MapToTournamentDto(tournament);
        }

        public async Task<TournamentDto> CreateAsync(CreateTournamentDto tournamentCreateDto)
        {
            var newTournament= new UserTournament
            {
                Id = Guid.NewGuid(),
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
                isPublic = tournamentCreateDto.isPublic,
                CreatedAt = DateTime.UtcNow
            };

            var createdTournamentEntity = await _tournamentRepository.CreateAsync(newTournament);

            return MapToTournamentDto(createdTournamentEntity);
        }

        public async Task<TournamentDto?> UpdateAsync(Guid tournamentId, UpdateTournamentDto tournamentToUpdateDto)
        {
            var tournamentToUpdate = new UserTournament
            {
                Id = tournamentId,
                Name = tournamentToUpdateDto.Name,
                Description = tournamentToUpdateDto.Description,
                FormatId = tournamentToUpdateDto.FormatId,
                NumberOfTeams = tournamentToUpdateDto.NumberOfTeams,
                MaxPlayersPerTeam = tournamentToUpdateDto.MaxPlayersPerTeam,
                StartDate = tournamentToUpdateDto.StartDate,
                EndDate = tournamentToUpdateDto.EndDate,
                Location = tournamentToUpdateDto.Location,
                AllowJoinViaLink = tournamentToUpdateDto.AllowJoinViaLink,
                OrganizerId = tournamentToUpdateDto.OrganizerId,
                BannerImage = tournamentToUpdateDto.BannerImage,
                ContactEmail = tournamentToUpdateDto.ContactEmail,
                ContactPhone = tournamentToUpdateDto.ContactPhone,
                EntryFee = tournamentToUpdateDto.EntryFee,
                MatchDuration = tournamentToUpdateDto.MatchDuration,
                RegistrationDeadline = tournamentToUpdateDto.RegistrationDeadline,
                isPublic = tournamentToUpdateDto.isPublic,
                CreatedAt = DateTime.UtcNow
            };

            var updatedTournament= await _tournamentRepository.UpdateAsync(tournamentId, tournamentToUpdate);

            if (updatedTournament == null)
            {
                return null;
            }

            return MapToTournamentDto(updatedTournament);
        }

        public async Task<bool> DeleteAsync(Guid tournamentId)
        {
            var deletedTournamentEntity = await _tournamentRepository.DeleteAsync(tournamentId);

            return deletedTournamentEntity != null;
        }

        private static TournamentDto MapToTournamentDto(UserTournament tournamentEntity) => new(
            tournamentEntity.Id,
            tournamentEntity.Name,
            tournamentEntity.Description,
            tournamentEntity.FormatId,
            tournamentEntity.NumberOfTeams,
            tournamentEntity.MaxPlayersPerTeam,
            tournamentEntity.StartDate,
            tournamentEntity.EndDate,
            tournamentEntity.Location,
            tournamentEntity.AllowJoinViaLink,
            tournamentEntity.OrganizerId,
            tournamentEntity.BannerImage,
            tournamentEntity.ContactEmail,
            tournamentEntity.ContactPhone,
            tournamentEntity.EntryFee,
            tournamentEntity.MatchDuration,
            tournamentEntity.RegistrationDeadline,
            tournamentEntity.isPublic,
            tournamentEntity.CreatedAt
        );

        public async Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync()
        {
            var formats = await _tournamentFormatService.GetFormatsAsync();

            return formats;
        }
    }
}
