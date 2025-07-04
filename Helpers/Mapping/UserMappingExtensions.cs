using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.Users;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class UserMappingExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                Role = user.Role?.Name ?? string.Empty,
                CreatedAt = user.CreatedAt
            };
        }

        public static UserListItemDto ToListItemDto(this User user)
        {
            return new UserListItemDto
            {
                Id = user.Id,
                FullName = $"{user.Name} {user.Surname}",
                ProfilePicture = user.ProfilePicture
            };
        }

        public static UserDetailDto ToDetailDto(this User user)
        {
            return new UserDetailDto
            {
                Id = user.Id,
                FullName = $"{user.Name} {user.Surname}",
                ProfilePicture = user.ProfilePicture,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Teams = user.TeamMemberships?.Select(tm => new UserTeamSummaryDto
                {
                    Id = tm.Team.Id,
                    Name = tm.Team.Name,
                    LogoUrl = tm.Team.LogoUrl,
                    IsCaptain = tm.Team.CaptainId == user.Id,
                    IsManager = tm.Team.ManagerId == user.Id
                }) ?? Array.Empty<UserTeamSummaryDto>()
            };
        }

        public static User ToEntity(this UserCreateDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                PasswordHash = dto.Password, // This should be hashed by the service
                ProfilePicture = dto.ProfilePicture,
                RoleId = dto.RoleId,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(this User user, UserUpdateDto dto)
        {
            if (dto.Name != null)
                user.Name = dto.Name;
                
            if (dto.Surname != null)
                user.Surname = dto.Surname;
                
            if (dto.Email != null)
                user.Email = dto.Email;
                
            if (dto.ProfilePicture != null)
                user.ProfilePicture = dto.ProfilePicture;
                
            user.UpdatedAt = DateTime.UtcNow;
        }
    }
}
