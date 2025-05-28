using Tournament.Management.API.Models.DTOs.User;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginUserAsync(LoginUserDto userDto);
        Task<string?> RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}