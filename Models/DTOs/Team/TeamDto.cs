namespace Tournament.Management.API.Models.DTOs.Team
{
    public record TeamDto(
        Guid Id,
        string Name,
        string LogoUrl,
        Guid ManagerId,
        Guid? CaptainId,
        bool IsActive
    );
}
