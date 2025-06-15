using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.User;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var usersDto = users.Select(user => ToUserDto(user));

            return usersDto;
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return null;

            return ToUserDto(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                return null;

            return ToUserDto(user);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto updatedData)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;

            var isUpdated = ApplyUserUpdates(user, updatedData);

            if (!isUpdated)
                return false;

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

        private static UserDto ToUserDto(User user)
        {
            return new UserDto(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.ProfilePicture,
                user.Role.Name
            );
        }
        
        private bool ApplyUserUpdates(User user, UserUpdateDto updatedData)
        {
            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(updatedData.Name) && updatedData.Name != user.Name)
            {
                user.Name = updatedData.Name;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updatedData.Surname) && updatedData.Surname != user.Surname)
            {
                user.Surname = updatedData.Surname;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updatedData.Email) && updatedData.Email != user.Email)
            {
                user.Email = updatedData.Email;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updatedData.ProfilePicture) && updatedData.ProfilePicture != user.ProfilePicture)
            {
                user.ProfilePicture = updatedData.ProfilePicture;
                isUpdated = true;
            }

            return isUpdated;
        }

    }
}