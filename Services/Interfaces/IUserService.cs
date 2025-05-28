using Tournament.Management.API.Models;
using Tournament.Management.API.Models.DTOs.User;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GettAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(int userId, UpdateUserDto user);
        Task<bool> DeleteUserAsync(int id);
    }
}