using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IUserService userService,
    ITeamService teamService,
    IPlayerStatService playerStatService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly ITeamService _teamService = teamService;
    private readonly IPlayerStatService _playerStatService = playerStatService;

    /// <summary>
    /// Get information about the currently logged-in user
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = GetUserId();
        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new { data = user });
    }

    /// <summary>
    /// Get a specific user by ID
    /// </summary>
    [HttpGet("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new { data = user });
    }

    /// <summary>
    /// Get detailed user profile by ID
    /// </summary>
    [HttpGet("{userId:guid}/profile")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile(Guid userId)
    {
        var profile = await _userService.GetUserDetailsByIdAsync(userId);
        if (profile is null)
        {
            return NotFound(new { message = "User profile not found." });
        }

        return Ok(new { data = profile });
    }

    /// <summary>
    /// Update the user's profile
    /// </summary>
    [HttpPut("{userId:guid}/profile")]
    [Authorize]
    public async Task<IActionResult> UpdateUserProfile(Guid userId, [FromBody] UserUpdateDto updateDto)
    {
        var currentUserId = GetUserId();
        if (currentUserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        var success = await _userService.UpdateUserAsync(userId, updateDto);
        if (!success)
        {
            return NotFound(new { message = "User not found or update failed." });
        }

        return Ok(new { message = "Profile updated successfully." });
    }

    /// <summary>
    /// Update the status of a user
    /// </summary>
    [HttpPatch("{userId:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserStatus(Guid userId, [FromBody] UpdateUserStatusDto statusDto)
    {
        var success = await _userService.UpdateUserStatusAsync(userId, statusDto.Status);
        if (!success)
        {
            return NotFound(new { message = "User not found or status update failed." });
        }

        return Ok(new { message = "User status updated successfully." });
    }

    /// <summary>
    /// Get player statistics for a specific user
    /// </summary>
    [HttpGet("{userId:guid}/stats")]
    [Authorize(Roles = "General")]
    public async Task<IActionResult> GetUserStats(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        var stats = await _playerStatService.GetPlayerStatsByPlayerIdAsync(userId);
        return Ok(new
        {
            data = new
            {
                userId,
                name = user.Name,
                surname = user.Surname,
                stats
            }
        });
    }

    /// <summary>
    /// Get all teams the user is a part of
    /// </summary>
    [Authorize(Roles = "General")]
    [HttpGet("{userId:guid}/teams")]
    public async Task<IActionResult> GetUserTeams(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        var teams = await _teamService.GetMyTeamsAsync(userId);
        return Ok(new { data = teams });
    }

    /// <summary>
    /// Search users by name, surname, or email
    /// </summary>
    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> SearchUsers([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
        {
            return BadRequest(new { message = "Search query must be at least 3 characters." });
        }

        var users = await _userService.GetUsersAsync();

        var results = users.Where(u =>
            u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            u.Surname.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            u.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        return Ok(new { data = results });
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
