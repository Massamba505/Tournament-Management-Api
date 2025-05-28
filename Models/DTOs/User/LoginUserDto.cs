using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User
{
    public record LoginUserDto(
        [Required] string Email,
        [Required, StringLength(250)] string Password
    );
}