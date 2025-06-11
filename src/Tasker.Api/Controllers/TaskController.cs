using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Tasks.Command.ChangeMember;
using Tasker.Application.Tasks.Command.ChangeName;
using Tasker.Application.Tasks.Command.ChangePriority;
using Tasker.Application.Tasks.Command.ChangeStatus;
using Tasker.Application.Tasks.Command.Create;
using Tasker.Application.Tasks.Command.Delete;
using Tasker.Application.Tasks.Query.GetMyTasks;
using Tasker.Application.Tasks.Query.GetProjectTasks;
using Tasker.Contracts.Task.ChangeMember;
using Tasker.Contracts.Task.ChangePriority;
using Tasker.Contracts.Task.ChangeStatus;
using Tasker.Contracts.Task.Create;
using Tasker.Contracts.Task.GetProjectTasks;
using Tasker.Contracts.Task.GetUserTasks;

namespace Tasker.Api.Controllers
{
    [Authorize]
    [Route("/api/task")]
    public class TaskController
        (IMediator mediator) : ApiController
    {

        #region Get Tasks By User

        [HttpGet("user")]
        public async Task<IActionResult> GetUserTasks()
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new GetMyTasksQuery(UserId));

            IEnumerable<GetUserTasksResponseDto> tasks =
                new List<GetUserTasksResponseDto>();

            if (result.Value is not null)
            {
                tasks = result.Value.Select(r => new GetUserTasksResponseDto(
                    r.Id,
                    r.Name,
                    r.Status,
                    r.Priority,
                    r.AssignedMemberId,
                    r.Deadline,
                    r.ProjectId)).ToList();
            }

            return result.Match(
                _ => Ok(tasks), Problem);
        }

        #endregion

        #region Get Tasks By Project

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectTasks([FromRoute] Guid projectId)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new GetProjectTasksQuery(projectId , UserId));

            IEnumerable<GetProjectTasksResponseDto> tasks =
                new List<GetProjectTasksResponseDto>();

            if (result.Value is not null)
            {
                tasks = result.Value.Select(r => new GetProjectTasksResponseDto(
                    r.Id,
                    r.Name,
                    r.Status,
                    r.Priority,
                    r.AssignedMemberId,
                    r.Deadline,
                    r.ProjectId)).ToList();
            }

            return result.Match(
                _ => Ok(tasks), Problem);
        }

        #endregion

        #region Create

        [HttpPost("{projectId}")]
        public async Task<IActionResult> Create(
            [FromRoute] Guid projectId,
            [FromBody] CreateTaskRequestDto request)
        {
            if (!TryGetUserId(out Guid CreatorId))
                return Unauthorized();

            var result = await mediator.Send
            (new CreateTaskCommand(
                request.name,
                request.status,
                request.priority,
                request.assignedMemberId,
                request.projectId,
                request.deadline));

            return result.Match(
                task => CreatedAtAction(
                    nameof(GetProjectTasks),
                    new { projectId = projectId, userId = CreatorId },
                    new CreateTaskResponseDto(
                        task.Id,
                        task.Name,
                        task.Status,
                        task.Priority,
                        task.AssignedMemberId,
                        task.Deadline,
                        task.ProjectId)), Problem);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new DeleteTaskCommand(UserId));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Change Assigned Member

        [HttpPatch("{taskId}/member")]
        public async Task<IActionResult> ChangeMember(
            [FromRoute] Guid taskId,
            [FromBody] TaskChangeMemberRequestDto request)
        {
            var result = await mediator.Send
                (new TaskChangeMemberCommand(taskId, request.userId));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Change Name

        [HttpPatch("{taskId}/{name}")]
        public async Task<IActionResult> ChangeName(
            [FromRoute] Guid taskId,
            [FromRoute] string name)
        {
            var result = await mediator.Send
                (new TaskChangeNameCommand(taskId, name));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Change Status

        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChangeStatus(
            [FromRoute] Guid taskId,
            [FromBody] TaskChangeStatusRequestDto request)
        {
            var result = await mediator.Send
                (new TaskChangeStatusCommand(taskId, request.status));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Change Priority

        [HttpPatch("{taskId}/priority")]
        public async Task<IActionResult> ChangePriority(
            [FromRoute] Guid taskId,
            [FromBody] TaskChangePriorityRequestDto request)
        {
            var result = await mediator.Send
                (new TaskChangePriorityCommand(taskId, request.priority));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion
    }
}
