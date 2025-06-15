using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Team
{
    public record TeamCreateDto
    (
        [Required] string Name,
        string? LogoUrl
    );

}
