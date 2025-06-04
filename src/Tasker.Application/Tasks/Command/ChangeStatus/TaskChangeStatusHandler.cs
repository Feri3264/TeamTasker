using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Command.ChangeStatus;

public class TaskChangeStatusHandler
    (ITaskRepository taskRepository) : IRequestHandler<TaskChangeStatusCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TaskChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        task.SetStatus(request.status);

        taskRepository.Update(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}