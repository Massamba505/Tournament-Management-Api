using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.User;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<User>> GettAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto updatedData)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;

            user.Name = updatedData.Name?? user.Name;
            user.Surname = updatedData.Surname ?? user.Surname;
            user.Email = updatedData.Email ?? user.Email;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return false;

            await _userRepository.DeleteUserAsync(user);
            return true;
        }
    }
}