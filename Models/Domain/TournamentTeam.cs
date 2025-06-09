namespace Tournament.Management.API.Models.Domain
{
    public class TournamentTeam
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public Guid TeamId { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsActive { get; set; }

        public UserTournament Tournament { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }

}
