using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;
using Tournament.Management.API.Helpers.Mapping;

namespace Tournament.Management.API.Services.Implementations
{
    public class UserService(IUserRepository userRepository, ITeamRepository teamRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDetailDto?> GetUserDetailsByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var teams = user.ManagedTeams.Select(t => t.ToSummaryDto())
                .Concat(user.CaptainedTeams.Select(t => t.ToSummaryDto()))
                .Concat(user.TeamMemberships.Select(tm => tm.Team.ToSummaryDto()))
                .Distinct();

            return user.ToDetailDto(teams);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user?.ToDto();
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto updatedData)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null)
            {
                return false;
            }

            user.UpdateFromDto(updatedData);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user is null)
            {
                return false;
            }

            await _userRepository.DeleteUserAsync(user);
            return true;
        }

        public async Task<bool> UpdateUserStatusAsync(Guid userId, UserStatus status)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null)
            {
                return false;
            }

            user.Status = status;
            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}