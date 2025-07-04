using Tournament.Management.API.Models.DTOs.TeamInvitations;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Services.Interfaces;

public interface ITeamInvitationService
{
    Task<IEnumerable<TeamInvitationDto>> GetTeamInvitationsAsync(Guid teamId);
    Task<IEnumerable<TeamInvitationDto>> GetUserInvitationsAsync(Guid userId);
    Task<TeamInvitationDto?> GetInvitationByIdAsync(Guid invitationId);
    Task<Guid> CreateInvitationAsync(TeamInvitationCreateDto invitation);
    Task<bool> RespondToInvitationAsync(Guid invitationId, InvitationStatus status);
    Task<bool> CancelInvitationAsync(Guid invitationId);
    Task<bool> DeleteInvitationAsync(Guid invitationId);
}
