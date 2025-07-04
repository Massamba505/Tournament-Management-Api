using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Mapping;

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

    public static User ToEntity(this UserRegisterDto registerDto)
    {
        return new User
        {
            Name = registerDto.Name,
            Surname = registerDto.Surname,
            Email = registerDto.Email,
            PasswordHash = registerDto.Password,
            ProfilePicture = registerDto.ProfilePicture,
            Role = registerDto.Role,
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void UpdateFromDto(this User user, UserUpdateDto updateDto)
    {
        if (updateDto.Name != null) user.Name = updateDto.Name;
        if (updateDto.Surname != null) user.Surname = updateDto.Surname;
        if (updateDto.Email != null) user.Email = updateDto.Email;
        if (updateDto.ProfilePicture != null) user.ProfilePicture = updateDto.ProfilePicture;
        user.UpdatedAt = DateTime.UtcNow;
    }
}
