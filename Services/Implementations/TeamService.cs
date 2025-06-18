using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Team;
using Tournament.Management.API.Repository.Implementations;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TeamService(ITeamRepository teamRepository) : ITeamService
    {
        private readonly ITeamRepository _teamRepository = teamRepository;

        public async Task<IEnumerable<TeamDto>> GetMyTeamsAsync(Guid userId)
        {
            var teams = await _teamRepository.GetTeamsByUserIdAsync(userId);
            return teams.Select(MapToDto);
        }

        public async Task<TeamDto?> GetTeamByIdAsync(Guid id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if(team is null)
            {
                return null;
            }

            return MapToDto(team);
        }

        public async Task CreateTeamAsync(Guid managerId, TeamCreateDto newTeam)
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = newTeam.Name,
                LogoUrl = newTeam.LogoUrl?? $"https://eu.ui-avatars.com/api/?name={newTeam.Name}&size=250",
                ManagerId = managerId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            await _teamRepository.CreateTeamAsync(team);
        }

        public async Task<bool> UpdateTeamAsync(Guid id, TeamUpdateDto team)
        {
            var existingTeam = await _teamRepository.GetTeamByIdAsync(id);
            if (existingTeam is null)
            {
                return false;
            }

            var isUpdated = ApplyTeamUpdates(existingTeam, team);

            if (!isUpdated)
            {
                return true;
            }

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

        public async Task<bool> DeactivateTeamAsync(Guid id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team is null)
            {
                return false;
            }

            await _teamRepository.DeactivateTeamAsync(team);
            return true;
        }

        public async Task<bool> ActivateTeamAsync(Guid id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            if (team is null)
            {
                return false;
            }

            await _teamRepository.ActivateTeamAsync(team);
            return true;
        }

        private bool ApplyTeamUpdates(Team existingTeam, TeamUpdateDto team)
        {
            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(team.Name) && team.Name != existingTeam.Name)
            {
                existingTeam.Name = team.Name;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(team.LogoUrl) && team.LogoUrl != existingTeam.LogoUrl)
            {
                existingTeam.LogoUrl = team.LogoUrl;
                isUpdated = true;
            }

            if (team.CaptainId != existingTeam.CaptainId)
            {
                existingTeam.CaptainId = team.CaptainId;
                isUpdated = true;
            }

            return isUpdated;
        }

        private TeamDto MapToDto(Team team)
        {
            return new TeamDto(
                team.Id,
                team.Name,
                team.LogoUrl,
                team.ManagerId,
                team.CaptainId,
                team.IsActive
            );
        }
    }

}
