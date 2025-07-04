using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user is null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new {data = user });
        }
    }
}
