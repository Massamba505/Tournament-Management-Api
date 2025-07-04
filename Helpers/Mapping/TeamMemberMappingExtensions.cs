using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Teams;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class TeamMemberMappingExtensions
    {
        public static TeamMemberDto ToDto(this TeamMember member)
        {
            return new TeamMemberDto
            {
                UserId = member.UserId,
                Name = member.User != null ? $"{member.User.Name} {member.User.Surname}" : string.Empty,
                ProfilePicture = member.User?.ProfilePicture,
                MemberType = member.MemberType,
                JoinedAt = member.JoinedAt
            };
        }
    }
}
