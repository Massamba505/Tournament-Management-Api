using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain
{
    public class Tournament
    {
        public Guid Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public TournamentFormatEnum Format { get; set; }
        
        [Range(2, 100)]
        public int NumberOfTeams { get; set; }
        
        [Range(1, 50)]
        public int MaxPlayersPerTeam { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        [Required, StringLength(200)]
        public string Location { get; set; } = string.Empty;
        
        public bool AllowJoinViaLink { get; set; }
        public Guid OrganizerId { get; set; }
        
        public string? BannerImage { get; set; }
        
        [EmailAddress]
        public string? ContactEmail { get; set; }
        
        [Phone]
        public string? ContactPhone { get; set; }
        
        [Range(0, 10000)]
        public decimal? EntryFee { get; set; }
        
        public int? MatchDuration { get; set; }
        
        public DateTime RegistrationDeadline { get; set; }
        
        public bool IsPublic { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public TournamentStatus Status { get; set; } = TournamentStatus.Draft;

        // Navigation properties
        public User Organizer { get; set; } = null!;
        public ICollection<TeamMatch> Matches { get; set; } = new List<TeamMatch>();
        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
    }
}
