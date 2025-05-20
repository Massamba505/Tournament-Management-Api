using Tournament.Management.API.Models;
using Tournament.Management.API.Models.Dto.User;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GettAllUsersAsync();
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task UpdateUserAsync(int userId, UpdateUserDto user);
        Task DeleteUserAsync(int id);
    }
}