using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User;

public record UserDto(
    Guid Id,

    [Required]
    [StringLength(50)]
    string Name,

    [Required]
    [StringLength(50)]
    string Surname,

    [Required]
    [EmailAddress]
    [StringLength(100)]
    string Email,

    [Required]
    [StringLength(100)]
    string ProfilePicture,

    [Required]
    string Role
);