using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.ChangePriority;

public class TaskChangePriorityHandler
    (ITaskRepository taskRepository) : IRequestHandler<TaskChangePriorityCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TaskChangePriorityCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        if (!Enum.TryParse<TaskPriorityEnum>(request.priority, true, out var priority))
        {
            return TaskError.PriorityNotValid;
        }

        task.SetPriority(priority.ToString());

        taskRepository.Update(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}