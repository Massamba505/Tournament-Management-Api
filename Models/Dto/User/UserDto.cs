using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.Dto
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }

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
        public int RoleId { get; set; }
    }
}