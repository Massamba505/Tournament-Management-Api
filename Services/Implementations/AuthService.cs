using Tournament.Management.API.Helpers.Interfaces;
using Tournament.Management.API.Models;
using Tournament.Management.API.Models.Dto.User;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHelper passwordHelper,
        ITokenService tokenService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IPasswordHelper _passwordHelper = passwordHelper;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<string?> LoginUserAsync(LoginUserDto userDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(userDto.Email);
            if (user == null)
            {
                return null;
            }

            var isPasswordValid = _passwordHelper.VerifyPassword(userDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return null;
            }

            return _tokenService.CreateToken(user);
        }

        public async Task<string?> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if (registerUserDto.RoleId == null)
            {
                throw new ArgumentException("RoleId is required");
            }

            var role = await _roleRepository.GetRoleByIdAsync(registerUserDto.RoleId.Value);
            if (role == null)
            {
                throw new ArgumentException("Invalid role");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email already in use");
            }

            var hashedPassword = _passwordHelper.HashPassword(registerUserDto.Password);

            var newUser = new User
            {
                Name = registerUserDto.Name,
                Surname = registerUserDto.Surname,
                Email = registerUserDto.Email,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateUserAsync(newUser);

            return _tokenService.CreateToken(newUser);
        }
    }
}
