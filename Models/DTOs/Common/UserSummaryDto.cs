namespace Tournament.Management.API.Models.DTOs.Common
{
    public record UserSummaryDto(
        Guid Id,
        string Name,
        string? ProfilePicture
    );
}
