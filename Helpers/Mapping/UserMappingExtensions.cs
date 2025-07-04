using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping
{
    public static class UserMappingExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            (
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.ProfilePicture,
                user.Role,
                user.CreatedAt
            );
        }

        public static UserDetailDto ToDetailDto(this User user, IEnumerable<TeamSummaryDto> teams)
        {
            return new UserDetailDto
            (
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.ProfilePicture,
                user.Role,
                user.CreatedAt,
                teams
            );
        }

        public static User ToEntity(this UserRegisterDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                PasswordHash = dto.Password, // This should be hashed by the service
                ProfilePicture = dto.ProfilePicture,
                Role = dto.Role,
                Status = UserStatus.Active,
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
