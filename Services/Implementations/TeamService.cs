using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Team;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TeamDto>> GetMyTeamsAsync(Guid userId)
        {
            var teams = await _repository.GetMyTeamsAsync(userId);
            return teams.Select(MapToDto);
        }

        public async Task<TeamDto?> GetByIdAsync(Guid id, Guid userId)
        {
            var team = await _repository.GetByIdAsync(id, userId);
            if(team == null)
            {
                return null;
            }

            return MapToDto(team);
        }

        public async Task CreateAsync(Guid managerId, TeamCreateDto dto)
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LogoUrl = dto.LogoUrl?? $"https://eu.ui-avatars.com/api/?name={dto.Name}&size=250",
                ManagerId = managerId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            await _repository.CreateAsync(team);
        }

        public async Task<bool> UpdateAsync(Guid id, TeamUpdateDto dto, Guid userId)
        {
            var existingTeam = await _repository.GetByIdAsync(id, userId);
            if (existingTeam is null)
                return false;

            var isUpdated = ApplyTeamUpdates(existingTeam, dto);

            if (!isUpdated)
                return false;

            await _repository.UpdateAsync(existingTeam);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var team = await _repository.GetByIdAsync(id, userId);
            if (team == null)
            {
                return false;
            }

            await _repository.DeleteAsync(team);
            return true;
        }

        public async Task<bool> DeactivateAsync(Guid id, Guid userId)
        {
            var team = await _repository.GetByIdAsync(id, userId);
            if (team == null)
            {
                return false;
            }

            await _repository.DeactivateAsync(team);
            return true;
        }

        public async Task<bool> ActivateAsync(Guid id, Guid userId)
        {
            var team = await _repository.GetByIdAsync(id, userId);
            if (team == null)
            {
                return false;
            }

            await _repository.ActivateAsync(team);
            return true;
        }

        private bool ApplyTeamUpdates(Team team, TeamUpdateDto dto)
        {
            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != team.Name)
            {
                team.Name = dto.Name;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.LogoUrl) && dto.LogoUrl != team.LogoUrl)
            {
                team.LogoUrl = dto.LogoUrl;
                isUpdated = true;
            }

            if (dto.CaptainId != team.CaptainId)
            {
                team.CaptainId = dto.CaptainId;
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
