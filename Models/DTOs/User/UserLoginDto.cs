using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User;

public record UserLoginDto(
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    string Email,

    [Required]
    string Password
);