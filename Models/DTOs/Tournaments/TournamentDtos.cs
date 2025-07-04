using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMatches;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Tournaments;

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
);

public record TournamentCreateDto(
    [Required, StringLength(100)]
    string Name,

    [Required]
    string Description,

    [Required] TournamentFormatEnum
        Format,
    
    [Range(2, 100)]
    int NumberOfTeams,

    [Range(1, 50)]
    int MaxPlayersPerTeam,

    [Required]
    DateTime StartDate,

    [Required]
    DateTime EndDate,

    [Required, StringLength(200)]
    string Location,

    bool AllowJoinViaLink,
    [Required] Guid OrganizerId,
    string? BannerImage,

    [EmailAddress]
    string? ContactEmail,

    [Phone]
    string? ContactPhone,

    [Range(0, 10000)]
    decimal? EntryFee,

    int? MatchDuration,
    [Required]
    DateTime RegistrationDeadline,

    bool IsPublic,
    TournamentStatus Status = TournamentStatus.Draft
);

public record TournamentUpdateDto(
    [StringLength(100)]
    string? Name,
    
    string? Description,
    DateTime? StartDate,
    DateTime? EndDate,
    
    [StringLength(200)]
    string? Location,
    
    bool? AllowJoinViaLink,
    string? BannerImage,
    
    [EmailAddress]
    string? ContactEmail,
    
    [Phone]
    string? ContactPhone,
    
    [Range(0, 10000)]
    decimal? EntryFee,

    int? MatchDuration,
    DateTime? RegistrationDeadline,
    bool? IsPublic,
    TournamentStatus? Status
);
