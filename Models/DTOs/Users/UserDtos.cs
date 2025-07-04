using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.Users;

public record UserDto(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string? ProfilePicture,
    UserRole Role,
    DateTime CreatedAt
);

public record UserDetailDto(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string? ProfilePicture,
    UserRole Role,
    DateTime CreatedAt,
    IEnumerable<TeamSummaryDto> Teams
);

public record UserUpdateDto(
    [StringLength(50)]
    string? Name,
    
    [StringLength(50)] string?
    Surname,
    
    [EmailAddress, StringLength(100)]
    string? Email,
    string? ProfilePicture
);

public record UserRegisterDto(
    [Required, StringLength(50)]
    string Name,
   
    [Required, StringLength(50)]
    string Surname,
    
    [Required, EmailAddress, StringLength(100)]
    string Email,
    
    [Required, StringLength(100, MinimumLength = 6)]
    string Password,
    string? ProfilePicture,
    UserRole Role
);

public record UserLoginDto(
    [Required]
    [EmailAddress, StringLength(100)]
    string Email,

    [Required]
    [MinLength(6)]
    string Password
);

public record UpdateUserStatusDto(
    [Required]
    UserStatus Status
);