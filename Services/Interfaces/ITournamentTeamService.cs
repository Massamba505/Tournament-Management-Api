using Tournament.Management.API.Models.DTOs.TournamentTeams;

namespace Tournament.Management.API.Services.Interfaces;

public interface ITournamentTeamService
{
    Task AddTournamentTeamAsync(Guid tournamentId, JoinTournamentDto join);
    Task<TournamentTeamDto?> GetTournamentTeamByTeamIdAsync(Guid tournamentId, Guid teamId);
    Task<TournamentTeamDetailDto?> GetTournamentTeamDetailsByTeamIdAsync(Guid tournamentId, Guid teamId);
    Task<IEnumerable<TournamentTeamDto>> GetTournamentTeamsByTournamentIdAsync(Guid tournamentId);
    Task<bool> RemoveTournamentTeamAsync(Guid tournamentId, Guid teamId);
}
