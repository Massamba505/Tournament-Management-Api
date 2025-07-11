using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournament.Management.API.Models.DTOs.Tournaments;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentsController(
    ITournamentService tournamentService,
    ITournamentFormatService formatService
) : ControllerBase
{
    private readonly ITournamentService _tournamentService = tournamentService;
    private readonly ITournamentFormatService _formatService = formatService;

    /// <summary>
    /// Get all tournaments
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTournaments()
    {
        var tournaments = await _tournamentService.GetTournamentsAsync();
        return Ok(new { data = tournaments });
    }

    /// <summary>
    /// Get tournament by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTournamentById(Guid id)
    {
        var tournament = await _tournamentService.GetTournamentByIdAsync(id);
        if (tournament is null)
            return NotFound(new { message = "Tournament not found." });

        return Ok(new { data = tournament });
    }

    /// <summary>
    /// Get detailed tournament info by ID (includes teams and matches)
    /// </summary>
    [HttpGet("{id:guid}/details")]
    public async Task<IActionResult> GetTournamentDetailsById(Guid id)
    {
        var tournament = await _tournamentService.GetTournamentDetailsByIdAsync(id);
        if (tournament is null)
            return NotFound(new { message = "Tournament not found." });

        return Ok(new { data = tournament });
    }

    /// <summary>
    /// Create a new tournament
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> CreateTournament([FromBody] TournamentCreateDto tournament)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _tournamentService.CreateTournamentAsync(tournament);
        return StatusCode(StatusCodes.Status201Created, new { message = "Tournament created." });
    }

    /// <summary>
    /// Update an existing tournament by ID
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> UpdateTournament(Guid id, [FromBody] TournamentUpdateDto tournament)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _tournamentService.UpdateTournamentAsync(id, tournament);
        if (!updated)
            return NotFound(new { message = "Tournament not updated." });

        return Ok(new { message = "Tournament updated." });
    }

    /// <summary>
    /// Update the status of a tournament
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> UpdateTournamentStatus(Guid id, [FromBody] UpdateTournamentStatusDto statusDto)
    {
        var updated = await _tournamentService.UpdateTournamentStatusAsync(id, statusDto.Status);
        if (!updated)
            return NotFound(new { message = "Tournament not found or status not updated." });

        return Ok(new { message = "Tournament status updated." });
    }

    /// <summary>
    /// Delete a tournament by ID
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> DeleteTournament(Guid id)
    {
        var result = await _tournamentService.DeleteTournamentAsync(id);
        if (!result)
            return NotFound(new { message = "Tournament not deleted." });

        return NoContent();
    }

    /// <summary>
    /// Get all available tournament formats
    /// </summary>
    [HttpGet("formats")]
    public async Task<IActionResult> GetTournamentFormats()
    {
        var formats = await _formatService.GetFormatsAsync();
        return Ok(new { data = formats });
    }

    /// <summary>
    /// Get tournaments created by a specific organizer
    /// </summary>
    [HttpGet("organizer/{id:guid}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> GetTournamentsByOrganizerId(Guid id)
    {
        var tournaments = await _tournamentService.GetTournamentsByOrganizerIdAsync(id);
        return Ok(new { data = tournaments });
    }

    /// <summary>
    /// Get tournaments by their status
    /// </summary>
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetTournamentsByStatus(TournamentStatus status)
    {
        var tournaments = await _tournamentService.GetTournamentsByStatusAsync(status);
        return Ok(new { data = tournaments });
    }

    /// <summary>
    /// Search for tournaments by keyword
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> SearchTournaments([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return BadRequest(new { message = "Search term cannot be empty." });

        var tournaments = await _tournamentService.SearchTournamentsAsync(term);
        return Ok(new { data = tournaments });
    }

    /// <summary>
    /// Get upcoming tournaments (limited count)
    /// </summary>
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingTournaments([FromQuery] int count = 5)
    {
        var tournaments = await _tournamentService.GetUpcomingTournamentsAsync(count);
        return Ok(new { data = tournaments });
    }

    /// <summary>
    /// Get tournaments a specific user has participated in
    /// </summary>
    [HttpGet("user/{userId:guid}/participation")]
    [Authorize]
    public async Task<IActionResult> GetTournamentsByUserParticipation(Guid userId)
    {
        if (userId != GetUserId() && !User.IsInRole("Admin"))
            return Forbid();

        var tournaments = await _tournamentService.GetTournamentsByUserParticipationAsync(userId);
        return Ok(new { data = tournaments });
    }

    /// <summary>
    /// Get tournaments the current logged in user has participated in
    /// </summary>
    [HttpGet("my-tournaments")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserTournaments()
    {
        var userId = GetUserId();
        var userRole = User.FindFirstValue(ClaimTypes.Role)!;

        if (userRole == UserRole.Organizer.ToString())
        {
            var Organizertournaments = await _tournamentService.GetTournamentsByOrganizerIdAsync(userId);
            return Ok(new { data = Organizertournaments });
        }

        var tournaments = await _tournamentService.GetTournamentsByUserParticipationAsync(userId);
        return Ok(new { data = tournaments });
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
