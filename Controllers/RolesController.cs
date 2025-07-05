using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Helpers.Extensions;
using Tournament.Management.API.Models.Enums;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IRoleRepository roleRepository) : ControllerBase
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            var roleInfoList = roles.Select(role => new
            {
                Id = (int)role,
                Name = role.GetDisplayName()
            });
            
            return Ok(roleInfoList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to retrieve roles", error = ex.Message });
        }
    }
}
