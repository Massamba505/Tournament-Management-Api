using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Team
{
    public record TeamUpdateDto
    (
        [Required]
        string Name,
        string? LogoUrl,
        Guid? CaptainId
    );
}
