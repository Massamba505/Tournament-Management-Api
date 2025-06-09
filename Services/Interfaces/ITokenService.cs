using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}