using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record CreateTournamentDto(
        [Required, StringLength(100)] string Name,
        [Required] string Description,
        [Required] int FormatId,
        [Required, Range(2, 128)] int NumberOfTeams,
        [Range(1, 25)] int MaxPlayersPerTeam,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required, StringLength(200)] string Location,
        bool AllowJoinViaLink,
        [Required] Guid OrganizerId,
        [Required] string BannerImage,
        [EmailAddress] string? ContactEmail,
        string? ContactPhone,
        [Range(0, 10000)] decimal? EntryFee,
        [Required, Range(0, 120)] int MatchDuration,
        [Required] DateTime RegistrationDeadline,
        [Required] bool isPublic
    );
}
