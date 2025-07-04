using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMatches;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TournamentMappingExtensions
    {
        public static TournamentDto ToDto(this UserTournament tournament)
        {
            return new TournamentDto(
                tournament.Id,
                tournament.Name,
                tournament.Description,
                tournament.Format.ToString(),
                tournament.NumberOfTeams,
                tournament.MaxPlayersPerTeam,
                tournament.StartDate,
                tournament.EndDate,
                tournament.Location,
                tournament.Organizer != null ? UserSummaryMappingExtensions.ToSummaryDto(tournament.Organizer, MemberType.Manager) :
                    new UserSummaryDto(Guid.Empty, string.Empty, null, MemberType.Manager),
                tournament.BannerImage,
                tournament.ContactEmail,
                tournament.ContactPhone,
                tournament.EntryFee,
                tournament.IsPublic,
                tournament.Status
            );
        }

        public static TournamentListItemDto ToListItemDto(this UserTournament tournament)
        {
            return new TournamentListItemDto(
                tournament.Id,
                tournament.Name,
                tournament.Format.ToString(),
                tournament.StartDate,
                tournament.EndDate,
                tournament.Location,
                tournament.Organizer != null ? $"{tournament.Organizer.Name} {tournament.Organizer.Surname}" : string.Empty,
                tournament.BannerImage,
                tournament.TournamentTeams?.Count ?? 0
            );
        }

        public static TournamentDetailDto ToDetailDto(this UserTournament tournament)
        {
            return new TournamentDetailDto(
                tournament.Id,
                tournament.Name,
                tournament.Description,
                tournament.Format.ToString(),
                tournament.NumberOfTeams,
                tournament.MaxPlayersPerTeam,
                tournament.StartDate,
                tournament.EndDate,
                tournament.Location,
                tournament.Organizer != null ? UserSummaryMappingExtensions.ToSummaryDto(tournament.Organizer, MemberType.Manager) :
                    new UserSummaryDto(Guid.Empty, string.Empty, null, MemberType.Manager),
                tournament.BannerImage,
                tournament.ContactEmail,
                tournament.ContactPhone,
                tournament.EntryFee,
                tournament.IsPublic,
                tournament.Status,
                tournament.TournamentTeams?.Select(tt => new TournamentTeamDto(
                    tt.TeamId,
                    tt.Team?.Name ?? string.Empty,
                    tt.Team?.LogoUrl,
                    tt.RegisteredAt
                )) ?? Array.Empty<TournamentTeamDto>(),
                tournament.Matches?.Select(m => TeamMatchMappingExtensions.ToDto(m)) ?? Array.Empty<MatchDto>(),
                tournament.RegistrationDeadline,
                tournament.AllowJoinViaLink,
                tournament.MatchDuration,
                tournament.CreatedAt
            );
        }

        public static UserTournament ToEntity(this TournamentCreateDto dto)
        {
            return new UserTournament
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

        public static void UpdateFromDto(this UserTournament tournament, TournamentUpdateDto dto)
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
                
            if (dto.Status.HasValue)
                tournament.Status = dto.Status.Value;
        }
    }
}
