namespace Tournament.Management.API.Models.DTOs.TournamentTeam
{
    public record TournamentTeamDto(
        Guid Id,
        Guid TeamId,
        string TeamName,
        string TeamLogoUrl,
        DateTime RegisteredAt,
        bool IsActive
    );
}
