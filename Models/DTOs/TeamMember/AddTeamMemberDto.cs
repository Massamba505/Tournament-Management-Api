using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.TeamMember
{
    public record AddTeamMemberDto(
        [Required] Guid UserId,
        [Required] int MemberId
    );

}
