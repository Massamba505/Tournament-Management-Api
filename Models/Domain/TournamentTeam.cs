using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.Domain;

public class TournamentTeam
{
    public Guid TournamentId { get; set; }
    public Guid TeamId { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public TeamStatus Status { get; set; } = TeamStatus.Active;

    public  UserTournament Tournament { get; set; } = null!;
    public Team Team { get; set; } = null!;
}
