using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Query.GetProjectTasks;

public class GetProjectTasksHandler
    (ITaskRepository taskRepository,
        IProjectRepository projectRepository) : IRequestHandler<GetProjectTasksQuery , ErrorOr<List<TaskModel>>>
{
    public async Task<ErrorOr<List<TaskModel>>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.projectId);

        if (project is null)
            return ProjectError.ProjectNotFound;

        var tasks = await taskRepository.GetByIdsAsync(project.TaskIds);

        if (tasks is null)
            return TaskError.TaskNotFound;

        return tasks;
    }
}