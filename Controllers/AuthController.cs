using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.Users;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Register a new user and return a JWT token if successful
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            return BadRequest(new
            {
                message = "Invalid data",
                errors
            });
        }

        var token = await _authService.RegisterUserAsync(userRegisterDto);

        if (token == null)
        {
            return BadRequest(new { message = "Please select a valid user role." });
        }

        return Ok(new
        {
            token,
            message = "Registered successfully"
        });
    }

    /// <summary>
    /// Login a user and return a JWT token if credentials are valid
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            return BadRequest(new
            {
                message = "Invalid login data",
                errors
            });
        }

        var token = await _authService.LoginUserAsync(userLoginDto);

        if (token == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(new
        {
            token,
            message = "Login successful"
        });
    }

    /// <summary>
    /// Logs out the current user
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public IActionResult LogoutUser()
    {
        return Ok(new { message = "Logged out successfully" });
    }
}
