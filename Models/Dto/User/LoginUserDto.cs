using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.Dto.User
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = null!;
    }
}