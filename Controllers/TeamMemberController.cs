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
        public async Task<IActionResult> GetMembers(Guid teamId)
        {
            var result = await _teamMemberService.GetMembersAsync(teamId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(Guid teamId, AddTeamMemberDto dto)
        {
             await _teamMemberService.AddMemberAsync(teamId, dto);
            return Ok("Player Added sucessfully");
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> RemoveMember(Guid teamId, Guid userId)
        {
            await _teamMemberService.RemoveMemberAsync(teamId, userId);
            return Ok("Player removed sucessfully");
        }

        [HttpPatch("{userId:guid}/captain/assign")]
        public async Task<IActionResult> AssignCaptain(Guid teamId, Guid userId)
        {
            await _teamMemberService.AssignCaptainAsync(teamId, userId);
            return Ok("Player updated sucessfully");
        }

        [HttpPatch("{userId:guid}/captain/unassign")]
        public async Task<IActionResult> UnassignCaptain(Guid teamId, Guid userId)
        {
            await _teamMemberService.UnassignCaptainAsync(teamId, userId);
            return Ok("Player updated sucessfully");
        }
    }

}
