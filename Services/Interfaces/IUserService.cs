using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDetailDto?> GetUserDetailsByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UpdateUserStatusAsync(Guid userId, UserStatus status);
    }
}