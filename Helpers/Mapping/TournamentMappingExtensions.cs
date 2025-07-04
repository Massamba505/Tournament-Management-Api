using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TournamentMappingExtensions
    {
        public static TournamentDto ToDto(this Tournament.Management.API.Models.Domain.Tournament tournament)
        {
            return new TournamentDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Description = tournament.Description,
                Format = tournament.Format.ToString(),
                NumberOfTeams = tournament.NumberOfTeams,
                MaxPlayersPerTeam = tournament.MaxPlayersPerTeam,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                Location = tournament.Location,
                Organizer = tournament.Organizer?.ToSummaryDto() ?? new UserSummaryDto(),
                BannerImage = tournament.BannerImage,
                ContactEmail = tournament.ContactEmail,
                ContactPhone = tournament.ContactPhone,
                EntryFee = tournament.EntryFee,
                IsPublic = tournament.IsPublic,
                Status = tournament.Status
            };
        }

        public static TournamentListItemDto ToListItemDto(this Tournament.Management.API.Models.Domain.Tournament tournament)
        {
            return new TournamentListItemDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Format = tournament.Format.ToString(),
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                Location = tournament.Location,
                OrganizerName = tournament.Organizer != null ? $"{tournament.Organizer.Name} {tournament.Organizer.Surname}" : string.Empty,
                BannerImage = tournament.BannerImage,
                TeamCount = tournament.TournamentTeams?.Count ?? 0
            };
        }

        public static TournamentDetailDto ToDetailDto(this Tournament.Management.API.Models.Domain.Tournament tournament)
        {
            var dto = new TournamentDetailDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Description = tournament.Description,
                Format = tournament.Format.ToString(),
                NumberOfTeams = tournament.NumberOfTeams,
                MaxPlayersPerTeam = tournament.MaxPlayersPerTeam,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                Location = tournament.Location,
                Organizer = tournament.Organizer?.ToSummaryDto() ?? new UserSummaryDto(),
                BannerImage = tournament.BannerImage,
                ContactEmail = tournament.ContactEmail,
                ContactPhone = tournament.ContactPhone,
                EntryFee = tournament.EntryFee,
                IsPublic = tournament.IsPublic,
                Teams = tournament.TournamentTeams?.Select(tt => TournamentTeamMappingExtensions.ToDto(tt)) ?? Array.Empty<TournamentTeamDto>(),
                Matches = tournament.Matches?.Select(m => TeamMatchMappingExtensions.ToDto(m)) ?? Array.Empty<MatchDto>(),
                RegistrationDeadline = tournament.RegistrationDeadline,
                AllowJoinViaLink = tournament.AllowJoinViaLink,
                MatchDuration = tournament.MatchDuration,
                CreatedAt = tournament.CreatedAt
            };
            
            return dto;
        }

        public static Tournament.Management.API.Models.Domain.Tournament ToEntity(this TournamentCreateDto dto)
        {
            return new Tournament.Management.API.Models.Domain.Tournament
            {
                Name = dto.Name,
                Description = dto.Description,
                Format = dto.Format,
                NumberOfTeams = dto.NumberOfTeams,
                MaxPlayersPerTeam = dto.MaxPlayersPerTeam,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                AllowJoinViaLink = dto.AllowJoinViaLink,
                OrganizerId = dto.OrganizerId,
                BannerImage = dto.BannerImage,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                EntryFee = dto.EntryFee,
                MatchDuration = dto.MatchDuration,
                RegistrationDeadline = dto.RegistrationDeadline,
                IsPublic = dto.IsPublic,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(this Tournament.Management.API.Models.Domain.Tournament tournament, TournamentUpdateDto dto)
        {
            if (dto.Name != null)
                tournament.Name = dto.Name;
                
            if (dto.Description != null)
                tournament.Description = dto.Description;
                
            if (dto.StartDate.HasValue)
                tournament.StartDate = dto.StartDate.Value;
                
            if (dto.EndDate.HasValue)
                tournament.EndDate = dto.EndDate.Value;
                
            if (dto.Location != null)
                tournament.Location = dto.Location;
                
            if (dto.AllowJoinViaLink.HasValue)
                tournament.AllowJoinViaLink = dto.AllowJoinViaLink.Value;
                
            if (dto.BannerImage != null)
                tournament.BannerImage = dto.BannerImage;
                
            if (dto.ContactEmail != null)
                tournament.ContactEmail = dto.ContactEmail;
                
            if (dto.ContactPhone != null)
                tournament.ContactPhone = dto.ContactPhone;
                
            if (dto.EntryFee.HasValue)
                tournament.EntryFee = dto.EntryFee;
                
            if (dto.MatchDuration.HasValue)
                tournament.MatchDuration = dto.MatchDuration;
                
            if (dto.RegistrationDeadline.HasValue)
                tournament.RegistrationDeadline = dto.RegistrationDeadline.Value;
                
            if (dto.IsPublic.HasValue)
                tournament.IsPublic = dto.IsPublic.Value;
        }
    }
}
