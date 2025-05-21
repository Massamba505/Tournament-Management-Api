using Tournament.Management.API.Models;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}