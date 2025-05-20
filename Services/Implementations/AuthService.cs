using Tournament.Management.API.Helpers;
using Tournament.Management.API.Models;
using Tournament.Management.API.Models.Dto.User;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class AuthService(IUserRepository userRepository, IRoleRepository roleRepository) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<string> LoginUserAsync(LoginUserDto userDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (user == null || !PasswordHelper.VerifyPassword(userDto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            return GenerateJwtToken(user);
        }

        public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var role = await _roleRepository.GetRoleByNameAsync(registerUserDto.RoleName);

            if (role == null)
            {
                throw new Exception("Invalid role");
            }

            var newUser = new User()
            {
                Name = registerUserDto.Name,
                Surname = registerUserDto.Surname,
                Email = registerUserDto.Email,
                PasswordHash = PasswordHelper.HashPassword(registerUserDto.Password),
                RoleId = role.Id,
                CreatedAt = DateTime.Now
            };

            await _userRepository.CreateUserAsync(newUser);

            return GenerateJwtToken(newUser);
        }

        private string GenerateJwtToken(User user)
        {
            return "this-token";
        }
    }
}