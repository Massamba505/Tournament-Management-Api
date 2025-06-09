namespace Tournament.Management.API.Models.Domain
{
    public class TeamMatch
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; } = null!;
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public UserTournament Tournament { get; set; } = null!;
        public Team HomeTeam { get; set; } = null!;
        public Team AwayTeam { get; set; } = null!;
        public ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();
    }

}
