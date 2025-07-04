using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.TeamMembers;

public record TeamMemberDto(
    Guid UserId,
    string FullName,
    MemberType MemberType,
    bool IsCaptain,
    DateTime JoinedAt
);

public record AddTeamMemberDto(
    [Required]
    Guid UserId,

    [Required]
    Guid TeamId
);

public record UpdateMemberTypeDto(
    [Required]
    MemberType MemberType
);
