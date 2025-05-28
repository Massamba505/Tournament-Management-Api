using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User
{
    public record RegisterUserDto
    (
        [Required, StringLength(50)] string Name,
        [Required, StringLength(50)] string Surname,
        [Required, EmailAddress, StringLength(100)] string Email,
        [Required, StringLength(255, MinimumLength = 6)] string Password,
        [Required] int? RoleId
    );
}