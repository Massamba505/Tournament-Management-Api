using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping;

public static class UserSummaryMappingExtensions
{
    public static UserSummaryDto ToSummaryDto(this User user, MemberType memberType = MemberType.Player)
    {
        return new UserSummaryDto(
            user.Id,
            $"{user.Name} {user.Surname}",
            user.ProfilePicture,
            memberType
        );
    }
}
