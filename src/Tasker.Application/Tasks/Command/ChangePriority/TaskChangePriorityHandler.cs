using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Command.ChangePriority;

public class TaskChangePriorityHandler
    (ITaskRepository taskRepository) : IRequestHandler<TaskChangePriorityCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TaskChangePriorityCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        task.SetPriority(request.priority);

        taskRepository.Update(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}