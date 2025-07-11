using Microsoft.OpenApi.Extensions;
using Tournament.Management.API.Helpers.Interfaces;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Services.Implementations;

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

    public async Task<string?> LoginUserAsync(UserLoginDto userDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(userDto.Email);
        if (user is null)
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

    public async Task<string?> RegisterUserAsync(UserRegisterDto registerUserDto)
    {
        var role = await _roleRepository.GetRoleByNameAsync(registerUserDto.Role.GetDisplayName());
        if (role !=  registerUserDto.Role)
        {
            throw new ArgumentException("Invalid role");
        }

        var existingUser = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);
        if (existingUser is not null)
        {
            throw new ArgumentException("Email alredyy exists, try to login");
        }

        var hashedPassword = _passwordHelper.HashPassword(registerUserDto.Password);

        var newUser = new User
        {
            Name = registerUserDto.Name,
            Surname = registerUserDto.Surname,
            Email = registerUserDto.Email,
            PasswordHash = hashedPassword,
            Role = role,
            ProfilePicture = $"https://eu.ui-avatars.com/api/?name={registerUserDto.Name}+{registerUserDto.Surname}&size=250",
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateUserAsync(newUser);
        return _tokenService.CreateToken(newUser);
    }
}
