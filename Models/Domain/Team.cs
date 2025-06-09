namespace Tournament.Management.API.Models.Domain
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public Guid ManagerId { get; set; }
        public Guid? CaptainId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public User Manager { get; set; } = null!;
        public User? Captain { get; set; }
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}
