using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Project.Command.ChangeName;
using Tasker.Application.Project.Command.Create;
using Tasker.Application.Project.Command.Delete;
using Tasker.Application.Project.Query.GetTeamProjects;
using Tasker.Contracts.Project.Create;
using Tasker.Contracts.Project.GetTeamProjects;

namespace Tasker.Api.Controllers
{
    [Route("/api/project")]
    public class ProjectController
        (IMediator mediator) : ApiController
    {

        #region Show Projects By Team

        [HttpGet("{teamId}/{userId}")]
        public async Task<IActionResult> GetTeamProjects(
            [FromRoute] Guid teamId,
            [FromRoute] Guid userId)
        {
            var result = await mediator.Send
                (new GetTeamProjectsQuery(teamId, userId));

            IEnumerable<GetTeamProjectsResponseDto> projects =
                new List<GetTeamProjectsResponseDto>();

            if (result.Value is not null)
            {
                projects = result.Value.Select(r => new GetTeamProjectsResponseDto(
                    r.Id,
                    r.Name,
                    r.LeadId)).ToList();
            }

            return result.Match(
                _ => Ok(projects), Problem);
        }

        #endregion

        #region Create

        [HttpPost("{teamId}/{creatorId}")]
        public async Task<IActionResult> Create(
            [FromRoute] Guid teamId,
            [FromRoute] Guid creatorId,
            [FromBody] CreateProjectRequestDto request)
        {
            var result = await mediator.Send
                (new CreateProjectCommand(request.name, teamId, request.leadId));

            return result.Match(
                project => CreatedAtAction(
                    nameof(GetTeamProjects),
                    new { teamId = teamId, userId = creatorId },
                    new CreateProjectResponseDto(
                        project.Id,
                        project.Name,
                        project.LeadId)), Problem);
        }

        #endregion

        #region Delete

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var result = await mediator.Send
                (new DeleteProjectCommand(Id));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Change Name

        [HttpPatch("{projectId}/{name}")]
        public async Task<IActionResult> ChangeName(
            [FromRoute] Guid projectId,
            [FromRoute] string name)
        {
            var result = await mediator.Send
                (new ProjectChangeNameCommand(projectId, name));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion
    }
}
