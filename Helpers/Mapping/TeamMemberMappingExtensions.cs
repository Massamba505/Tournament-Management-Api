using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TeamMembers;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TeamMemberMappingExtensions
    {
        public static TeamMemberDto ToDto(this TeamMember member)
        {
            return new TeamMemberDto(
                member.UserId,
                member.User != null ? $"{member.User.Name} {member.User.Surname}" : string.Empty,
                member.MemberType,
                member.Team?.CaptainId == member.UserId,
                member.JoinedAt
            );
        }
    }
}
