using System.ComponentModel.DataAnnotations;
using Tournament.Management.API.Models.DTOs.Common;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Models.DTOs.TeamInvitations;

public enum InvitationStatus
{
    Pending,
    Accepted,
    Rejected,
    Expired
}

public record TeamInvitationDto(
    Guid Id,
    Guid TeamId,
    string TeamName,
    Guid InvitedUserId,
    UserSummaryDto InvitedUser,
    Guid InvitedByUserId,
    UserSummaryDto InvitedByUser,
    InvitationStatus Status,
    DateTime CreatedAt,
    DateTime? RespondedAt
);

public record TeamInvitationCreateDto(
    [Required] Guid TeamId,
    [Required] Guid InvitedUserId
);

public record TeamInvitationResponseDto(
    [Required] InvitationStatus Status
);
