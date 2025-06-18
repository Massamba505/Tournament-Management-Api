using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record UpdateTournamentDto(
        [Required] string Name,
        [Required] string Description,
        [Required] int FormatId,
        [Range(2, 100)] int NumberOfTeams,
        [Range(1, 50)] int MaxPlayersPerTeam,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required] string Location,
        bool AllowJoinViaLink,
        string BannerImage,
        [EmailAddress] string? ContactEmail,
        string? ContactPhone,
        [Range(0, double.MaxValue)] decimal? EntryFee,
        [Range(0, 180)] int? MatchDuration,
        [Required] DateTime RegistrationDeadline,
        bool IsPublic
    );
}
