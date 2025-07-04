using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain
{
    public class TeamMember
    {
        // Use composite key (will be configured in EntityTypeConfiguration)
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
        public MemberType MemberType { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
