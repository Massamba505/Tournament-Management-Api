using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMember;
using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TeamMappingExtensions
    {
        public static TeamDto ToDto(this Team team)
        {
            return new TeamDto(
                team.Id,
                team.Name,
                team.LogoUrl,
                team.Manager != null ? UserSummaryMappingExtensions.ToSummaryDto(team.Manager, MemberType.Manager) : 
                    new UserSummaryDto(Guid.Empty, string.Empty, null, MemberType.Manager),
                team.Captain != null ? UserSummaryMappingExtensions.ToSummaryDto(team.Captain, MemberType.Captain) : null,
                team.Status,
                team.CreatedAt
            );
        }

        public static TeamDetailDto ToDetailDto(this Team team)
        {
            return new TeamDetailDto(
                team.Id,
                team.Name,
                team.LogoUrl,
                team.Manager != null ? $"{team.Manager.Name} {team.Manager.Surname}" : string.Empty,
                team.Status,
                team.Manager != null ? UserSummaryMappingExtensions.ToSummaryDto(team.Manager, MemberType.Manager) : 
                    new UserSummaryDto(Guid.Empty, string.Empty, null, MemberType.Manager),
                team.Captain != null ? UserSummaryMappingExtensions.ToSummaryDto(team.Captain, MemberType.Captain) : null,
                team.Members?.Select(m => new TeamMemberDto(
                    m.UserId, 
                    m.User != null ? $"{m.User.Name} {m.User.Surname}" : string.Empty,
                    m.MemberType,
                    team.CaptainId.HasValue && team.CaptainId.Value == m.UserId,
                    m.JoinedAt
                )) ?? Array.Empty<TeamMemberDto>(),
                team.CreatedAt
            );
        }

        public static Team ToEntity(this TeamCreateDto dto, Guid managerId)
        {
            return new Team
            {
                Name = dto.Name,
                LogoUrl = dto.LogoUrl,
                ManagerId = managerId,
                Status = TeamStatus.Active,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static TeamSummaryDto ToSummaryDto(this Team team)
        {
            return new TeamSummaryDto
            (
                team.Id,
                team.Name,
                team.LogoUrl
            );
        }

        public static void UpdateFromDto(this Team team, TeamUpdateDto dto)
        {
            if (dto.Name != null)
                team.Name = dto.Name;
                
            if (dto.LogoUrl != null)
                team.LogoUrl = dto.LogoUrl;
                
            if (dto.CaptainId.HasValue)
                team.CaptainId = dto.CaptainId;
                
            if (dto.Status.HasValue)
                team.Status = dto.Status.Value;
            
            team.UpdatedAt = DateTime.UtcNow;
        }
    }
}
