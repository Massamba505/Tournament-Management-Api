using Tournament.Management.API.Models.DTOs.User;

namespace Tournament.Management.API.Models.DTOs.Team
{
    public record TeamDto(
        Guid Id,
        string Name,
        string LogoUrl,
        Manager Manager,
        Captain? Captain
    );
}
