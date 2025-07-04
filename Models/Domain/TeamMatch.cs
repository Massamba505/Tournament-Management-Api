using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain
{
    public class TeamMatch
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        
        public DateTime MatchDate { get; set; }
        
        [Required, StringLength(200)]
        public string Venue { get; set; } = string.Empty;
        
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        
        public MatchStatus Status { get; set; } = MatchStatus.Scheduled;
        
        public int? Round { get; set; }

        // Navigation properties
        public Tournament Tournament { get; set; } = null!;
        public Team HomeTeam { get; set; } = null!;
        public Team AwayTeam { get; set; } = null!;
        public ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();
    }
}
