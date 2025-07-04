using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Teams
{
    public record TeamDto(
        Guid Id,
        string Name,
        string? LogoUrl,
        UserSummaryDto Manager,
        UserSummaryDto? Captain,
        TeamStatus Status,
        DateTime CreatedAt
    );

    public record TeamListItemDto(
        Guid Id,
        string Name,
        string? LogoUrl,
        string ManagerName,
        TeamStatus Status
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
    ) : TeamListItemDto(Id, Name, LogoUrl, ManagerName, Status);

    public record TeamCreateDto(
        [Required, StringLength(100)] string Name,
        string? LogoUrl,
        [Required] Guid ManagerId,
        Guid? CaptainId
    );

    public record TeamUpdateDto(
        [StringLength(100)] string? Name,
        string? LogoUrl,
        Guid? CaptainId,
        TeamStatus? Status
    );

    public record TeamMemberDto(
        Guid UserId,
        string Name,
        string? ProfilePicture,
        MemberType MemberType,
        DateTime JoinedAt
    );
}
