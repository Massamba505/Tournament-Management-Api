using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain;

public class TeamMember
{
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public MemberType MemberType { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Team Team { get; set; } = null!;
}
