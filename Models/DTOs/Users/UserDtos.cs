using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Users
{
    public record UserDto(
        Guid Id,
        string Name,
        string Surname,
        string Email,
        string? ProfilePicture,
        UserRole Role,
        DateTime CreatedAt
    );

    public record UserListItemDto(
        Guid Id,
        string FullName,
        string? ProfilePicture
    );

    public record UserDetailDto(
        Guid Id,
        string FullName,
        string? ProfilePicture,
        string Email,
        DateTime CreatedAt,
        IEnumerable<UserTeamSummaryDto> Teams
    ) : UserListItemDto(Id, FullName, ProfilePicture);

    public record UserCreateDto(
        [Required, StringLength(50)] string Name,
        [Required, StringLength(50)] string Surname,
        [Required, EmailAddress, StringLength(100)] string Email,
        [Required, StringLength(100, MinimumLength = 6)] string Password,
        string? ProfilePicture,
        UserRole Role = UserRole.General
    );

    public record UserUpdateDto(
        [StringLength(50)] string? Name,
        [StringLength(50)] string? Surname,
        [EmailAddress, StringLength(100)] string? Email,
        string? ProfilePicture
    );

    public record UserTeamSummaryDto(
        Guid Id,
        string Name,
        string? LogoUrl,
        bool IsCaptain,
        bool IsManager
    ) : TeamSummaryDto(Id, Name, LogoUrl);
}
