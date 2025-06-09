namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record TournamentDto(
        Guid TournamentId,
        string Name,
        string Description,
        int FormatId,
        int NumberOfTeams,
        int MaxPlayersPerTeam,
        DateTime StartDate,
        DateTime EndDate,
        string Location,
        bool AllowJoinViaLink,
        Guid OrganizerId,
        string BannerImage,
        string? ContactEmail,
        string? ContactPhone,
        decimal? EntryFee,
        int? MatchDuration,
        DateTime RegistrationDeadline,
        bool isPublic,
        DateTime CreatedAt
    );
}
