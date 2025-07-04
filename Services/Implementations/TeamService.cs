using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations
{
    public class TeamService(ITeamRepository teamRepository, ITeamMemberRepository teamMemberRepository) : ITeamService
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;

        public async Task<IEnumerable<TeamDto>> GetMyTeamsAsync(Guid userId)
        {
            var teams = await _teamRepository.GetTeamsByUserIdAsync(userId);
            return teams.Select(t => t.ToDto());
        }

        public async Task<TeamDto?> GetTeamByIdAsync(Guid id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            return team?.ToDto();
        }

        public async Task<TeamDetailDto?> GetTeamDetailsByIdAsync(Guid id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team == null)
            {
                return null;
            }

            return team.ToDetailDto();
        }

        public async Task CreateTeamAsync(Guid managerId, TeamCreateDto newTeam)
        {
            var team = newTeam.ToEntity(managerId);
            await _teamRepository.CreateTeamAsync(team);
        }

        public async Task<bool> UpdateTeamAsync(Guid id, TeamUpdateDto team)
        {
            var existingTeam = await _teamRepository.GetTeamByIdAsync(id);
            if (existingTeam is null)
            {
                return false;
            }

            existingTeam.UpdateFromDto(team);
            await _teamRepository.UpdateTeamAsync(existingTeam);
            return true;
        }

        public async Task<bool> DeleteTeamAsync(Guid id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team is null)
            {
                return false;
            }

            await _teamRepository.DeleteTeamAsync(team);
            return true;
        }

        public async Task<bool> UpdateTeamStatusAsync(Guid id, TeamStatus status)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team is null)
            {
                return false;
            }

            await _teamRepository.UpdateTeamStatusAsync(team, status);
            return true;
        }

        public async Task<IEnumerable<TeamDto>> GetTeamsByStatusAsync(TeamStatus status)
        {
            var teams = await _teamRepository.GetTeamsByStatusAsync(status);
            return teams.Select(t => t.ToDto());
        }
    }
}
