using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.ProjectMember.Command.Create;
using Tasker.Application.ProjectMember.Command.Delete;
using Tasker.Application.ProjectMember.Query.GetProjectMembers;
using Tasker.Contracts.ProjectMember.AddProjectMember;
using Tasker.Contracts.ProjectMember.GetProjectMembers;

namespace Tasker.Api.Controllers
{
    [Route("/api/project")]
    public class ProjectMemberController
        (IMediator mediator) : ApiController
    {

        #region Show Members By Project

        [HttpGet("{projectId}/member")]
        public async Task<IActionResult> GetMembers([FromRoute] Guid projectId)
        {
            var result = await mediator.Send
                (new GetProjectMembersQuery(projectId));

            IEnumerable<GetProjectMembersResponseDto> members =
                new List<GetProjectMembersResponseDto>();

            if (result.Value is not null)
            {
                members = result.Value.Select(r => new GetProjectMembersResponseDto(
                    r.Id,
                    r.Name,
                    r.Email)).ToList();
            }

            return result.Match(
                _ => Ok(members), Problem);
        }

        #endregion

        #region Add Member To Project

        [HttpPost("{projectId}/member")]
        public async Task<IActionResult> AddMember(
            [FromRoute] Guid projectId,
            [FromBody] AddProjectMemberRequestDto request)
        {
            var result = await mediator.Send
                (new CreateProjectMemberCommand(projectId, request.userId));

            return result.Match(
                projectMember => CreatedAtAction(
                    nameof(GetMembers),
                    new { projectId = projectId },
                    new AddProjectMemberResponseDto(
                        projectMember.Id,
                        projectMember.ProjectId,
                        projectMember.UserId)), Problem);
        }


        #endregion

        #region Remove Member From Project

        [HttpDelete("{projectId}/member/{memberId}")]
        public async Task<IActionResult> RemoveMember(
            [FromRoute] Guid projectId,
            [FromRoute] Guid memberId)
        {
            var result = await mediator.Send
                (new DeleteProjectMemberCommand(projectId, memberId));

            return result.Match(
                _ => Ok(), Problem);
        }


        #endregion
    }
}
