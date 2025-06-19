using Tournament.Management.API.Models.DTOs.TournamentTeam;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITournamentTeamService
    {
        Task AddTournamentTeamAsync(Guid tournamentId, JoinTournamentDto join);
        Task<TournamentTeamDto?> GetTournamentTeamByTeamIdAsync(Guid tournamentId, Guid id);
        Task<IEnumerable<TournamentTeamDto>> GetTournamentTeamsByTournamentIdAsync(Guid tournamentId);
        Task<bool> RemoveTournamentTeamAsync(Guid tournamentId, Guid id);
    }
}
