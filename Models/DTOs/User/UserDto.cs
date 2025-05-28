using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User
{
    public record UserDto(
        [Required] int Id,
        [Required, StringLength(50)] string Name,
        [Required, StringLength(50)] string Surname,
        [Required, EmailAddress, StringLength(100)] string Email,
        [Required] int RoleId
    );
}