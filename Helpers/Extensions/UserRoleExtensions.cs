using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Helpers.Extensions;

public static class UserRoleExtensions
{
    public static string GetDisplayName(this UserRole role)
    {
        return role.ToString();
    }
    
    public static UserRole ToUserRole(this string roleNameOrId)
    {
        if (Enum.TryParse<UserRole>(roleNameOrId, true, out var role))
        {
            return role;
        }
        
        if (int.TryParse(roleNameOrId, out int roleValue) && 
            Enum.IsDefined(typeof(UserRole), roleValue))
        {
            return (UserRole)roleValue;
        }
        
        return UserRole.General;
    }
}
