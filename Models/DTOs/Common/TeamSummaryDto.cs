namespace Tournament.Management.API.Models.DTOs.Common;

public record TeamSummaryDto(
    Guid Id,
    string Name,
    string? LogoUrl
);
