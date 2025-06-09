namespace Tournament.Management.API.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string ProfilePicture { get; set; } = null!;
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Role Role { get; set; } = null!;
    public ICollection<Team> ManagedTeams { get; set; } = new List<Team>();
    public ICollection<Team> CaptainedTeams { get; set; } = new List<Team>();
    public ICollection<TeamMember> TeamMemberships { get; set; } = new List<TeamMember>();
}
