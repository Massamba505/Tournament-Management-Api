using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.Dto.User
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Surname { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        public int? RoleId { get; set; }
    }
}