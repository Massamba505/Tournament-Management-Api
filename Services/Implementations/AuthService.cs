using Tournament.Management.API.Helpers.Interfaces;
using Tournament.Management.API.Models;
using Tournament.Management.API.Models.Dto.User;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations
{
    public class AuthService(
            IUserRepository userRepository,
            IPasswordHelper passwordHelper,
            ITokenService tokenService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHelper _passwordHelper = passwordHelper;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<string?> LoginUserAsync(LoginUserDto userDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (user == null || !_passwordHelper.VerifyPassword(userDto.Password, user.PasswordHash))
            {
                return null;
            }

            return _tokenService.CreateToken(user);
        }

        public async Task<string?> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var newUser = new User()
            {
                Name = registerUserDto.Name,
                Surname = registerUserDto.Surname,
                Email = registerUserDto.Email,
                PasswordHash = _passwordHelper.HashPassword(registerUserDto.Password),
                RoleId = registerUserDto.RoleId,
                CreatedAt = DateTime.Now
            };

            await _userRepository.CreateUserAsync(newUser);

            return _tokenService.CreateToken(newUser);
        }
    }
}