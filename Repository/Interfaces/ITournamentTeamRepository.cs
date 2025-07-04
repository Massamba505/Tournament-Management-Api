using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface ITournamentTeamRepository
    {
        Task<TournamentTeam?> GetTournamentTeamByTeamIdAsync(Guid tournamentId, Guid teamId);
        Task<IEnumerable<TournamentTeam>> GetTournamentTeamsByTournamentIdAsync(Guid tournamentId);
        Task AddTournamentTeamAsync(TournamentTeam tournamentTeam);
        Task UpdateTournamentTeamAsync(TournamentTeam tournamentTeam);
        Task DeleteTournamentTeamAsync(TournamentTeam tournamentTeam);
    }
}
