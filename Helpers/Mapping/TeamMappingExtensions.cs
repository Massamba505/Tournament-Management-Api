using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.TeamMembers;
using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping;

public static class TeamMappingExtensions
{
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto(
            team.Id,
            team.Name,
            team.LogoUrl,
            team.GetManager(),
            team.GetCaptain(),
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
            $"{team.Manager.Name} {team.Manager.Surname}",
            team.Status,
            team.GetManager(),
            team.GetCaptain(),
            team.GetMembers(),
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
        if (dto.Name is not null)
            team.Name = dto.Name;
            
        if (dto.LogoUrl is not null)
            team.LogoUrl = dto.LogoUrl;
            
        if (dto.CaptainId.HasValue)
            team.CaptainId = dto.CaptainId;
            
        if (dto.Status.HasValue)
            team.Status = dto.Status.Value;
    }

    private static UserSummaryDto GetManager(this Team team)
    {
        return UserSummaryMappingExtensions.ToSummaryDto(team.Manager, MemberType.Manager);
    }

    private static UserSummaryDto? GetCaptain(this Team team)
    {
        if(team.Captain is null)
        {
            return null;
        }

        return UserSummaryMappingExtensions.ToSummaryDto(team.Captain, MemberType.Manager);
    }

    private static IEnumerable<TeamMemberDto> GetMembers(this Team team)
    {
        if (team.Members == null)
            return [];
            
        return team.Members.Select(m => new TeamMemberDto(
            m.UserId,
            $"{m.User?.Name} {m.User?.Surname}".Trim(),
            m.MemberType,
            team.CaptainId.HasValue && team.CaptainId.Value == m.UserId,
            m.JoinedAt
        ));
    }

}
