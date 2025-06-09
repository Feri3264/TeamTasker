using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.TeamMember.Command.Create;
using Tasker.Application.TeamMember.Command.Delete;
using Tasker.Application.TeamMember.Query.GetTeamMembers;
using Tasker.Contracts.TeamMember.AddTeamMember;
using Tasker.Contracts.TeamMember.GetTeamMembers;

namespace Tasker.Api.Controllers
{
    [Route("/api/team")]
    public class TeamMemberController
        (IMediator mediator) : ApiController
    {

        #region Show Members By Team

        [HttpGet("{teamId:guid}/member")]
        public async Task<IActionResult> GetMembers([FromRoute] Guid teamId)
        {
            var result = await mediator.Send
                (new GetTeamMembersQuery(teamId));

            IEnumerable<GetTeamMembersResponseDto> members =
                new List<GetTeamMembersResponseDto>();

            if (result.Value is not null)
            {
                members = result.Value.Select(r => new GetTeamMembersResponseDto(
                    r.Id,
                    r.Name,
                    r.Email)).ToList();
            }

            return result.Match(
                _ => Ok(members), Problem);
        }

        #endregion

        #region Add Member To Team

        [HttpPost("{teamId}/member")]
        public async Task<IActionResult> AddMember(
            [FromRoute] Guid teamId,
            [FromBody] AddTeamMemberRequestDto request)
        {
            var result = await mediator.Send
                (new CreateTeamMemberCommand(teamId, request.userId));

            return result.Match(
                teamMember => CreatedAtAction(
                    nameof(GetMembers),
                    new { teamId = teamId },
                    new AddTeamMemberResponseDto(
                        teamMember.Id,
                        teamMember.TeamId,
                        teamMember.UserId)), Problem);
        }


        #endregion

        #region Remove Member From Team

        [HttpDelete("{teamId}/member/{memberId}")]
        public async Task<IActionResult> RemoveMember(
            [FromRoute] Guid teamId,
            [FromRoute] Guid memberId)
        {
            var result = await mediator.Send
                (new DeleteTeamMemberCommand(teamId, memberId));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion
    }
}
