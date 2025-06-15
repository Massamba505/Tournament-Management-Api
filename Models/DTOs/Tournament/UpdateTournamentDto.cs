using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record UpdateTournamentDto(
        [StringLength(100)] string Name,
        string Description,
        int FormatId,
        [Range(2, 128)] int NumberOfTeams,
        [Range(1, 25)] int MaxPlayersPerTeam,
        DateTime StartDate,
        DateTime EndDate,
        [StringLength(200)] string Location,
        bool AllowJoinViaLink,
        Guid OrganizerId,
        string BannerImage,
        [EmailAddress] string ContactEmail,
        string ContactPhone,
        [Range(0, 10000)] decimal EntryFee,
        [Range(0, 120)] int MatchDuration,
        DateTime RegistrationDeadline,
        bool IsPublic
    );
}
