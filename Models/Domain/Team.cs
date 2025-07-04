using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain
{
    public class Team
    {
        public Guid Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public string? LogoUrl { get; set; }
        
        public Guid ManagerId { get; set; }
        public Guid? CaptainId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TeamStatus Status { get; set; } = TeamStatus.Active;

        // Navigation properties
        public User Manager { get; set; } = null!;
        public User? Captain { get; set; }
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}
