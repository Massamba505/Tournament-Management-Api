using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMatches;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping;

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
            tournament.RegistrationDeadline,
            tournament.Location,
            tournament.GetOrganizer(),
            tournament.BannerImage ?? "",
            tournament.ContactEmail,
            tournament.ContactPhone,
            tournament.EntryFee,
            tournament.IsPublic,
            tournament.Status,
            tournament.CreatedAt
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
            tournament.RegistrationDeadline,
            tournament.Location,
            tournament.GetOrganizer(),
            tournament.BannerImage ?? "",
            tournament.ContactEmail,
            tournament.ContactPhone,
            tournament.EntryFee,
            tournament.IsPublic,
            tournament.Status,
            tournament.AllowJoinViaLink,
            tournament.MatchDuration,
            tournament.CreatedAt,
            tournament.GetTeams(),
            tournament.GetMatches()
        );
    }

    public static UserTournament ToEntity(this TournamentCreateDto dto)
    {
        return new UserTournament
        {
            Name = dto.Name,
            Description = dto.Description,
            Format = dto.Format,
            NumberOfTeams = dto.MaxNumberOfTeams,
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
        if (dto.Name is not null)
            tournament.Name = dto.Name;
            
        if (dto.Description is not null)
            tournament.Description = dto.Description;
            
        if (dto.StartDate.HasValue)
            tournament.StartDate = dto.StartDate.Value;
            
        if (dto.EndDate.HasValue)
            tournament.EndDate = dto.EndDate.Value;
            
        if (dto.Location is not null)
            tournament.Location = dto.Location;
            
        if (dto.AllowJoinViaLink.HasValue)
            tournament.AllowJoinViaLink = dto.AllowJoinViaLink.Value;
            
        if (dto.BannerImage is not null)
            tournament.BannerImage = dto.BannerImage;
            
        if (dto.ContactEmail is not null)
            tournament.ContactEmail = dto.ContactEmail;
            
        if (dto.ContactPhone is not null)
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

    private static UserSummaryDto GetOrganizer(this UserTournament tournament)
    {
        return UserSummaryMappingExtensions.ToSummaryDto(tournament.Organizer, MemberType.Organizer);
    }

    private static IEnumerable<TournamentTeamDto> GetTeams(this UserTournament tournament)
    {
        return tournament.TournamentTeams
               .Select(tt => new TournamentTeamDto(
                    tt.TeamId,
                    tt.Team.Name,
                    tt.Team.LogoUrl,
                    tt.RegisteredAt
            )) ?? [];
    }

    private static IEnumerable<MatchDto> GetMatches(this UserTournament tournament)
    {
        return tournament.Matches.Select(m => TeamMatchMappingExtensions.ToDto(m)) ?? [];
    }
}
