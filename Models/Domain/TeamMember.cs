namespace Tournament.Management.API.Models.Domain
{
    public class TeamMember
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
        public bool IsCaptain { get; set; }
        public int MemberId { get; set; }
        public DateTime JoinedAt { get; set; }

        public User User { get; set; } = null!;
        public Team Team { get; set; } = null!;
        public Member Member { get; set; } = null!;
    }
}
