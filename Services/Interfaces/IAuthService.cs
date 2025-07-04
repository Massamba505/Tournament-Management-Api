using Tournament.Management.API.Models.DTOs.Users;

namespace Tournament.Management.API.Services.Interfaces;

public interface IAuthService
{
    Task<string?> LoginUserAsync(UserLoginDto userDto);
    Task<string?> RegisterUserAsync(UserRegisterDto registerUserDto);
}