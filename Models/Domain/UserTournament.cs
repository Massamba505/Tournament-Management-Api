using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tournament.Management.API.Models.Domain
{
    public class UserTournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int FormatId { get; set; }
        public int NumberOfTeams { get; set; }
        public int MaxPlayersPerTeam { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = null!;
        public bool AllowJoinViaLink { get; set; }
        public Guid OrganizerId { get; set; }
        public string BannerImage { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public decimal? EntryFee { get; set; }
        public int? MatchDuration { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public bool isPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Organizer { get; set; } = null!;
        public TournamentFormat Format { get; set; } = null!;
        public ICollection<TeamMatch> Matches { get; set; } = new List<TeamMatch>();
        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
    }
}
