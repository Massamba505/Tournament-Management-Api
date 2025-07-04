using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TeamMembers;

namespace Tournament.Management.API.Helpers.Mapping;

public static class TeamMemberMappingExtensions
{
    public static TeamMemberDto ToDto(this TeamMember member)
    {
        return new TeamMemberDto(
            member.UserId,
            $"{member.User.Name} {member.User.Surname}",
            member.MemberType,
            member.Team?.CaptainId == member.UserId,
            member.JoinedAt
        );
    }
}
