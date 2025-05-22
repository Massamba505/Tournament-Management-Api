using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int roleId);
    }
}