using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Common;

public record UserSummaryDto(
    Guid Id,
    string FullName,
    string? ProfilePicture,
    MemberType MemberType
);
