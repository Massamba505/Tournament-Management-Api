using Tournament.Management.API.Models;
using Tournament.Management.API.Models.Dto.User;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Invalid User id");
            }

            await _userRepository.DeleteUserAsync(user);
        }

        public Task<IEnumerable<User>> GettAllUsersAsync()
        {
            return _userRepository.GetAllUsersAsync();
        }

        public Task<User?> GetUserById(int id)
        {
            var user = _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Invalid id");
            }

            return user;
        }

        public Task<User?> GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid email");
            }

            return user;
        }

        public async Task UpdateUserAsync(int userId, UpdateUserDto user)
        {
            var userToUpdate = await _userRepository.GetUserByIdAsync(userId);
            if (userToUpdate == null)
            {
                throw new Exception("Invalid user");
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Email = user.Email;

            await _userRepository.UpdateUserAsync(userToUpdate);
            await _userRepository.UpdateUserAsync(userToUpdate);
        }
    }
}