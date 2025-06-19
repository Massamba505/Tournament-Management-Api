using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tournament.Management.API.Models.DTOs.TeamMember;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API.Controllers
{
    [Route("api/teams/{teamId:guid}/members")]
    [ApiController]
    public class TeamMembersController(ITeamMemberService teamMemberService) : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService = teamMemberService;

        [HttpGet]
        public async Task<IActionResult> GetTeamMembers(Guid teamId)
        {
            var teamMembers = await _teamMemberService.GetTeamMembersAsync(teamId);
            return Ok(new { data = teamMembers });
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamMember(Guid teamId, AddTeamMemberDto teamMember)
        {
             await _teamMemberService.AddTeamMemberAsync(teamId, teamMember);
            return Ok(new {message = "Player Added sucessfully" });
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> RemoveTeamMember(Guid teamId, Guid userId)
        {
            await _teamMemberService.RemoveTeamMemberAsync(teamId, userId);
            return Ok(new { message = "Player removed sucessfully" });
        }

        [HttpPatch("{userId:guid}/captain/assign")]
        public async Task<IActionResult> AssignTeamCaptain(Guid teamId, Guid userId)
        {
            await _teamMemberService.AssignTeamCaptainAsync(teamId, userId);
            return Ok(new { message = "Player updated sucessfully" });
        }

        [HttpPatch("{userId:guid}/captain/unassign")]
        public async Task<IActionResult> UnassignTeamCaptain(Guid teamId, Guid userId)
        {
            await _teamMemberService.UnassignTeamCaptainAsync(teamId, userId);
            return Ok(new { message = "Player updated sucessfully" });
        }
    }

}
