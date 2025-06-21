using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Team;
using Tournament.Management.API.Models.DTOs.Tournament;
using Tournament.Management.API.Models.DTOs.TournamentTeam;
using Tournament.Management.API.Models.DTOs.User;

namespace Tournament.Management.API.Helper
{
    public static class DtoMapper
    {
        public static TeamDto MapToTeamDto(Team team)
        {
            string managerFullName = $"{team.Manager.Name} {team.Manager.Surname}";
            var manager = new Manager(team.Manager.Id, managerFullName, team.Manager.ProfilePicture);

            Captain? captain = null;
            if (team.Captain is not null)
            {
                var captainFullName = $"{team.Captain.Name} {team.Captain.Surname}";
                captain = new Captain(team.Captain.Id, captainFullName, team.Captain.ProfilePicture);
            }

            return new TeamDto(team.Id, team.Name, team.LogoUrl, manager, captain);
        }

        public static TournamentTeamDto MapToTournamentTeamDto(TournamentTeam tournamentTeam)
        {
            TeamDto team = MapToTeamDto(tournamentTeam.Team);
            return new TournamentTeamDto(team, tournamentTeam.RegisteredAt);
        }

        public static TournamentDto MapToTournamentDto(UserTournament tournament)
        {
            return new TournamentDto(
                tournament.Id,
                tournament.Name,
                tournament.Description,
                Format: tournament.Format.Name,
                tournament.NumberOfTeams,
                tournament.MaxPlayersPerTeam,
                tournament.StartDate,
                tournament.EndDate,
                tournament.Location,
                tournament.AllowJoinViaLink,
                tournament.OrganizerId,
                tournament.BannerImage,
                tournament.ContactEmail,
                tournament.ContactPhone,
                tournament.EntryFee,
                tournament.MatchDuration,
                tournament.RegistrationDeadline,
                tournament.isPublic,
                tournament.CreatedAt
            );
        }
    }
}
