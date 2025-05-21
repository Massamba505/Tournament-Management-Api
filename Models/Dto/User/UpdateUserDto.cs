using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.Dto.User
{
    public class UpdateUserDto
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
    }
}