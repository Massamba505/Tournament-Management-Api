using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.User;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}