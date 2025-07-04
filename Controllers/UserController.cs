using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Models.Enums;
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

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return Unauthorized(new { message = "Invalid token." });
        }

        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new { data = user });
    }

    [Authorize]
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new { data = user });
    }

    [Authorize]
    [HttpGet("{userId:guid}/profile")]
    public async Task<IActionResult> GetUserProfile(Guid userId)
    {
        var userDetails = await _userService.GetUserDetailsByIdAsync(userId);
        if (userDetails is null)
        {
            return NotFound(new { message = "User profile not found." });
        }

        return Ok(new { data = userDetails });
    }

    [Authorize]
    [HttpPut("{userId:guid}/profile")]
    public async Task<IActionResult> UpdateUserProfile(Guid userId, [FromBody] UserUpdateDto updateDto)
    {
        var currentUserId = GetUserId();
        if (currentUserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        var result = await _userService.UpdateUserAsync(userId, updateDto);
        if (!result)
        {
            return NotFound(new { message = "User not found or update failed." });
        }

        return Ok(new { message = "Profile updated successfully." });
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{userId:guid}/status")]
    public async Task<IActionResult> UpdateUserStatus(Guid userId, [FromBody] UpdateUserStatusDto statusDto)
    {
        var result = await _userService.UpdateUserStatusAsync(userId, statusDto.Status);
        if (!result)
        {
            return NotFound(new { message = "User not found or status update failed." });
        }

        return Ok(new { message = "User status updated successfully." });
    }

    [Authorize]
    [HttpGet("{userId:guid}/stats")]
    public async Task<IActionResult> GetUserStats(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        var playerStats = await _playerStatService.GetPlayerStatsByPlayerIdAsync(userId);
        
        return Ok(new { 
            data = new {
                userId,
                name = user.Name,
                surname = user.Surname,
                stats = playerStats
            } 
        });
    }

    [Authorize]
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

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
        {
            return BadRequest(new { message = "Search query must be at least 3 characters." });
        }

        var users = await _userService.GetUsersAsync();
        var filteredUsers = users.Where(u => 
            u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || 
            u.Surname.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            u.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
        
        return Ok(new { data = filteredUsers });
    }

    private Guid GetUserId()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId) 
            ? Guid.Empty 
            : userId;
    }
}
