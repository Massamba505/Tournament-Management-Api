using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentTeams;
using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TournamentTeamMappingExtensions
    {
        public static TournamentTeamDto ToDto(this TournamentTeam tournamentTeam)
        {
            return new TournamentTeamDto(
                tournamentTeam.TeamId,
                tournamentTeam.Team?.Name ?? string.Empty,
                tournamentTeam.Team?.LogoUrl,
                tournamentTeam.RegisteredAt
            );
        }
        
        public static TournamentTeamDetailDto ToDetailDto(this TournamentTeam tournamentTeam)
        {
            if (tournamentTeam.Team == null)
                throw new InvalidOperationException("Cannot create TournamentTeamDetailDto without a Team");
                
            return new TournamentTeamDetailDto(
                TeamMappingExtensions.ToDto(tournamentTeam.Team),
                tournamentTeam.RegisteredAt
            );
        }
    }
}
