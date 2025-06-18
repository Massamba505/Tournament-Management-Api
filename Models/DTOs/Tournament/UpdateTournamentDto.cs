using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record UpdateTournamentDto(
        [Required, StringLength(100)] string Name,
        string? Description,
        [Required] int FormatId,
        [Required, Range(2, 128)] int NumberOfTeams,
        [Required, Range(1, 25)] int MaxPlayersPerTeam,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required, StringLength(200)] string Location,
        [Required] bool AllowJoinViaLink,
        [Required] Guid OrganizerId,
        [Required] string BannerImage,
        [EmailAddress] string? ContactEmail,
        string? ContactPhone,
        [Range(0, 10000)] decimal? EntryFee,
        [Range(0, 120)] int? MatchDuration,
        [Required] DateTime RegistrationDeadline,
        [Required] bool IsPublic
    );
}
