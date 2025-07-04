using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class UserSummaryMappingExtensions
    {
        public static UserSummaryDto ToSummaryDto(this User user)
        {
            return new UserSummaryDto
            {
                Id = user.Id,
                Name = $"{user.Name} {user.Surname}",
                ProfilePicture = user.ProfilePicture
            };
        }
    }
}
