using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Tournaments
{
    public record TournamentDto(
        Guid Id,
        string Name,
        string Description,
        string Format,
        int NumberOfTeams,
        int MaxPlayersPerTeam,
        DateTime StartDate,
        DateTime EndDate,
        string Location,
        UserSummaryDto Organizer,
        string? BannerImage,
        string? ContactEmail,
        string? ContactPhone,
        decimal? EntryFee,
        bool IsPublic,
        TournamentStatus Status
    );

    public record TournamentListItemDto(
        Guid Id,
        string Name,
        string Format,
        DateTime StartDate,
        DateTime EndDate,
        string Location,
        string OrganizerName,
        string? BannerImage,
        int TeamCount
    );

    public record TournamentDetailDto(
        Guid Id,
        string Name,
        string Description,
        string Format,
        int NumberOfTeams,
        int MaxPlayersPerTeam,
        DateTime StartDate,
        DateTime EndDate,
        string Location,
        UserSummaryDto Organizer,
        string? BannerImage,
        string? ContactEmail,
        string? ContactPhone,
        decimal? EntryFee,
        bool IsPublic,
        TournamentStatus Status,
        IEnumerable<TournamentTeamDto> Teams,
        IEnumerable<MatchDto> Matches,
        DateTime RegistrationDeadline,
        bool AllowJoinViaLink,
        int? MatchDuration,
        DateTime CreatedAt
    ) : TournamentDto(Id, Name, Description, Format, NumberOfTeams, MaxPlayersPerTeam, StartDate, EndDate, Location, Organizer, BannerImage, ContactEmail, ContactPhone, EntryFee, IsPublic, Status);

    public record TournamentCreateDto(
        [Required, StringLength(100)] string Name,
        [Required] string Description,
        [Required] TournamentFormatEnum Format,
        [Range(2, 100)] int NumberOfTeams,
        [Range(1, 50)] int MaxPlayersPerTeam,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required, StringLength(200)] string Location,
        bool AllowJoinViaLink,
        [Required] Guid OrganizerId,
        string? BannerImage,
        [EmailAddress] string? ContactEmail,
        [Phone] string? ContactPhone,
        [Range(0, 10000)] decimal? EntryFee,
        int? MatchDuration,
        [Required] DateTime RegistrationDeadline,
        bool IsPublic,
        TournamentStatus Status = TournamentStatus.Draft
    );

    public record TournamentUpdateDto(
        [StringLength(100)] string? Name,
        string? Description,
        DateTime? StartDate,
        DateTime? EndDate,
        [StringLength(200)] string? Location,
        bool? AllowJoinViaLink,
        string? BannerImage,
        [EmailAddress] string? ContactEmail,
        [Phone] string? ContactPhone,
        [Range(0, 10000)] decimal? EntryFee,
        int? MatchDuration,
        DateTime? RegistrationDeadline,
        bool? IsPublic,
        TournamentStatus? Status
    );

    public record MatchDto(
        Guid Id,
        TeamSummaryDto HomeTeam,
        TeamSummaryDto AwayTeam,
        int? HomeScore,
        int? AwayScore,
        DateTime MatchDate,
        string Venue,
        MatchStatus Status,
        int? Round
    );

    public record TournamentTeamDto(
        Guid TeamId,
        string TeamName,
        string? LogoUrl,
        DateTime RegisteredAt,
        TeamStatus Status
    );

    public record TeamSummaryDto(
        Guid Id,
        string Name,
        string? LogoUrl
    );
}
