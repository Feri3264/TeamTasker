using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;
using Tasker.Domain.User;

namespace Tasker.Application.Tasks.Query.GetMyTasks;

public class GetMyTasksHandler
    (IUserRepository userRepository,
        ITaskRepository taskRepository) : IRequestHandler<GetMyTasksQuery , ErrorOr<List<TaskModel>>>
{
    public async Task<ErrorOr<List<TaskModel>>> Handle(GetMyTasksQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.id);

        if (user is null)
            return UserError.UserNotFound;

        var tasks = await taskRepository.GetByIdsAsync(user.TaskIds);

        if (tasks is null)
            return TaskError.TaskNotFound;

        return tasks;
    }
}