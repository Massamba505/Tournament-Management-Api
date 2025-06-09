using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.User;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GettAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}