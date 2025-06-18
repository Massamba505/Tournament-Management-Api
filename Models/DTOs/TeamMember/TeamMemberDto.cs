namespace Tournament.Management.API.Models.DTOs.TeamMember
{
    public record TeamMemberDto(
        Guid UserId,
        string FullName,
        string Email,
        string MemberType,
        bool IsCaptain,
        DateTime JoinedAt
    );
}
