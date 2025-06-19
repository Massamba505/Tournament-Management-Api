using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TeamMember;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class TeamMemberService(ITeamMemberRepository teamMemberRepository, ITeamRepository teamRepository) : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;

        public async Task<IEnumerable<TeamMemberDto>> GetTeamMembersAsync(Guid teamId)
        {
            var members = await _teamMemberRepository.GetTeamMembersByTeamIdAsync(teamId);
            return members.Select(MapToDto);
        }

        public async Task AddTeamMemberAsync(Guid teamId, AddTeamMemberDto newTeamMember)
        {
            var teamMember = new TeamMember
            {
                Id = Guid.NewGuid(),
                TeamId = teamId,
                UserId = newTeamMember.UserId,
                MemberId = newTeamMember.MemberId,
                IsCaptain = false
            };

            await _teamMemberRepository.AddTeamMemberAsync(teamMember);
        }

        public async Task<bool> RemoveTeamMemberAsync(Guid teamId, Guid userId)
        {
            var teamMember = await _teamMemberRepository.GetTeamMemberByTeamIdAsync(teamId, userId);
            if(teamMember is null)
            {
                return false;
            }

            await _teamMemberRepository.RemoveTeamMemberAsync(teamMember);
            return true;
        }

        public async Task<bool> AssignTeamCaptainAsync(Guid teamId, Guid userId)
        {
            return await ApplyUpdateToCaptainAsync(teamId, userId, true);
        }

        public async Task<bool> UnassignTeamCaptainAsync(Guid teamId, Guid userId)
        {
            return await ApplyUpdateToCaptainAsync(teamId, userId, false);
        }

        private async Task<bool> ApplyUpdateToCaptainAsync(Guid teamId, Guid userId, bool status)
        {
            var teamMember = await _teamMemberRepository.GetTeamMemberByTeamIdAsync(teamId, userId);
            if (teamMember is null)
            {
                return false;
            }

            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (team is null)
            {
                return false;
            }

            if (teamMember.IsCaptain == status)
            {
                return true;
            }


            if (status)
            {
                var teamMembers = await _teamMemberRepository.GetTeamMembersByTeamIdAsync(teamId);

                var currentCaptain = teamMembers.FirstOrDefault(tm => tm.IsCaptain);
                if (currentCaptain is not null && currentCaptain.UserId != userId)
                {
                    currentCaptain.IsCaptain = false;
                    await _teamMemberRepository.UpdateTeamMemberAsync(currentCaptain);
                }

                teamMember.IsCaptain = true;
                team.CaptainId = userId;
            }
            else
            {
                teamMember.IsCaptain = false;

                if (team.CaptainId == userId)
                {
                    team.CaptainId = null;
                }
            }

            await _teamMemberRepository.UpdateTeamMemberAsync(teamMember);
            await _teamRepository.UpdateTeamAsync(team);
            return true;
        }

        private TeamMemberDto MapToDto(TeamMember teamMemberDto)
        {
            return new TeamMemberDto(
                    teamMemberDto.UserId,
                    $"{teamMemberDto.User.Name} {teamMemberDto.User.Surname}",
                    teamMemberDto.User.Email,
                    teamMemberDto.Member.Name,
                    teamMemberDto.IsCaptain,
                    teamMemberDto.JoinedAt
                );
        }
    }

}
