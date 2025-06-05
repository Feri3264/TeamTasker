using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Query.GetProjectTasks;

public class GetProjectTasksHandler
    (ITaskRepository taskRepository,
        ITeamRepository teamRepository,
        ISessionRepository sessionRepository,
        IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository) : IRequestHandler<GetProjectTasksQuery , ErrorOr<List<TaskModel>>>
{
    public async Task<ErrorOr<List<TaskModel>>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.projectId);

        if (project is null)
            return ProjectError.ProjectNotFound;

        var team = await teamRepository.GetByIdAsync(project.TeamId);

        var session = await sessionRepository.GetByIdAsync(team.SessionId);

        var membership = await projectMemberRepository
            .GetProjectMemberAsync(request.userId, request.projectId);

        var tasks = new List<TaskModel>();

        if (session.OwnerId == request.userId || session.Editors.Contains(request.userId) ||
            team.LeadId == request.userId || project.LeadId == request.userId ||
            membership is not null)
        {
            tasks = await taskRepository.GetByIdsAsync(project.TaskIds);
        }
        else
        {
            return ProjectMemberError.MembershipNotFound;
        }

        if (tasks is null)
            return TaskError.TaskNotFound;

        return tasks;
    }
}