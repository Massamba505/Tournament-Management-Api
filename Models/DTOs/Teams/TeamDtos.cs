using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMembers;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Teams;

public record TeamDto(
    Guid Id,
    string Name,
    string? LogoUrl,
    UserSummaryDto Manager,
    UserSummaryDto? Captain,
    TeamStatus Status,
    DateTime CreatedAt
);

public record TeamDetailDto(
    Guid Id,
    string Name,
    string? LogoUrl,
    string ManagerName,
    TeamStatus Status,
    UserSummaryDto Manager,
    UserSummaryDto? Captain,
    IEnumerable<TeamMemberDto> Members,
    DateTime CreatedAt
);

public record TeamCreateDto(
    [Required, StringLength(100)]
    string Name,
    string? LogoUrl
);

public record TeamUpdateDto(
    [StringLength(100)]
    string? Name,
    string? LogoUrl,
    Guid? CaptainId,
    TeamStatus? Status
);

