using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.TeamMember;

public record TeamMemberDto(
    Guid UserId,
    string FullName,
    MemberType MemberType,
    bool IsCaptain,
    DateTime JoinedAt
);
