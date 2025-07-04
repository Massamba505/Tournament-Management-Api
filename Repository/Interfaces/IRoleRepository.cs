using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Repository.Interfaces;

public interface IRoleRepository
{
    Task<UserRole> GetRoleByNameAsync(string roleName);
    Task<IEnumerable<UserRole>> GetAllRolesAsync();
    Task<string> GetRoleNameAsync(UserRole role);
    Task<bool> IsValidRoleAsync(UserRole role);
}