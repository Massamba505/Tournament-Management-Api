using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required, StringLength(50)]
    public string Surname { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public UserRole Role { get; set; } = UserRole.General;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Team> ManagedTeams { get; set; } = new List<Team>();
    public ICollection<Team> CaptainedTeams { get; set; } = new List<Team>();
    public ICollection<TeamMember> TeamMemberships { get; set; } = new List<TeamMember>();
}
