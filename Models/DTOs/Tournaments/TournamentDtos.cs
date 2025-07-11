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
    int MaxNumberOfTeams,
    int MaxPlayersPerTeam,
    DateTime StartDate,
    DateTime EndDate,
    DateTime RegistrationDeadline,
    string Location,
    UserSummaryDto Organizer,
    string BannerImage,
    string? ContactEmail,
    string? ContactPhone,
    decimal? EntryFee,
    bool IsPublic,
    TournamentStatus Status,
    DateTime CreatedAt
);

public record TournamentDetailDto(
    Guid Id,
    string Name,
    string Description,
    string Format,
    int MaxNumberOfTeams,
    int MaxPlayersPerTeam,
    DateTime StartDate,
    DateTime EndDate,
    DateTime RegistrationDeadline,
    string Location,
    UserSummaryDto Organizer,
    string BannerImage,
    string? ContactEmail,
    string? ContactPhone,
    decimal? EntryFee,
    bool IsPublic,
    TournamentStatus Status,
    bool AllowJoinViaLink,
    int? MatchDuration,
    DateTime CreatedAt,
    IEnumerable<TournamentTeamDto> Teams,
    IEnumerable<MatchDto> Matches
);

public record TournamentCreateDto(
    [Required, StringLength(100)]
    string Name,

    [Required]
    string Description,

    [Required]
    TournamentFormatEnum Format,
    
    [Range(2, 100)]
    int MaxNumberOfTeams,

    [Range(1, 50)]
    int MaxPlayersPerTeam,

    [Required]
    DateTime StartDate,

    [Required]
    DateTime EndDate,

    [Required]
    DateTime RegistrationDeadline,

    [Required, StringLength(200)]
    string Location,

    
    [Required] 
    Guid OrganizerId,
    
    string BannerImage,

    [EmailAddress]
    string? ContactEmail,

    [Phone]
    string? ContactPhone,

    [Range(0, 10000)]
    decimal? EntryFee,
    
    bool AllowJoinViaLink,

    int? MatchDuration,

    bool IsPublic,
    TournamentStatus Status = TournamentStatus.Draft
);

public record TournamentUpdateDto(
    [StringLength(100)]
    string? Name,
    string? Description,
    DateTime? StartDate,
    DateTime? EndDate,
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

public record UpdateTournamentStatusDto(
    [Required]
    TournamentStatus Status
);
