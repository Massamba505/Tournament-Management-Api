using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations;

public class RoleRepository : IRoleRepository
{
    public Task<UserRole> GetRoleByNameAsync(string roleName)
    {
        if (Enum.TryParse<UserRole>(roleName, true, out var role))
        {
            return Task.FromResult(role);
        }
        
        if (int.TryParse(roleName, out int roleValue) && 
            Enum.IsDefined(typeof(UserRole), roleValue))
        {
            return Task.FromResult((UserRole)roleValue);
        }
        
        return Task.FromResult(UserRole.General);
    }
    
    public Task<IEnumerable<UserRole>> GetAllRolesAsync()
    {
        return Task.FromResult<IEnumerable<UserRole>>(Enum.GetValues<UserRole>());
    }
    
    public Task<string> GetRoleNameAsync(UserRole role)
    {
        return Task.FromResult(role.ToString());
    }
    
    public Task<bool> IsValidRoleAsync(UserRole role)
    {
        bool isValid = Enum.IsDefined(role);
        return Task.FromResult(isValid);
    }
}