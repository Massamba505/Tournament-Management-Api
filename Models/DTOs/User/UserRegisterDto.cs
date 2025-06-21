using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User;

public record UserRegisterDto(
    [Required]
    [MaxLength(50)]
    string Name,

    [Required]
    [MaxLength(50)]
    string Surname,

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    string Email,

    [Required]
    [MinLength(6)]
    string Password,

    string? ProfilePicture,

    [Required]
    string Role
);