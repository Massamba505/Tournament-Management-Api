using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TournamentTeamMappingExtensions
    {
        public static TournamentTeamDto ToDto(this TournamentTeam tournamentTeam)
        {
            return new TournamentTeamDto
            {
                TeamId = tournamentTeam.TeamId,
                TeamName = tournamentTeam.Team?.Name ?? string.Empty,
                LogoUrl = tournamentTeam.Team?.LogoUrl,
                RegisteredAt = tournamentTeam.RegisteredAt,
                Status = tournamentTeam.Status
            };
        }
    }
}
