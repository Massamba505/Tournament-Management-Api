﻿namespace Tournament.Management.API.Models.DTOs.Tournament
{
    public record TournamentDto(
        Guid Id,
        string Name,
        string Description,
        string Format,
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
        bool IsPublic,
        DateTime CreatedAt
    );
}
