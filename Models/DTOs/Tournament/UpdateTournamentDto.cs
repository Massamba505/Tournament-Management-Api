using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record UpdateTournamentDto
    (
        [Required, MaxLength(100)] string Name,
        [Required, MaxLength(500)] string Description,
        [Required] int FormatId,
        [Range(2, 100)] int NumberOfTeams,
        [Range(1, 50)] int MaxPlayersPerTeam,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required, MaxLength(200)] string Location,
        bool AllowJoinViaLink,
        string? BannerImage,
        [EmailAddress] string? ContactEmail,
        [Phone] string? ContactPhone,
        [Range(0, double.MaxValue)] decimal? EntryFee,
        [Range(1, 180)] int? MatchDuration,
        [Required] DateTime RegistrationDeadline,
        bool IsPublic
    );


}
