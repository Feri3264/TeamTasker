using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.ChangeStatus;

public class TaskChangeStatusHandler
    (ITaskRepository taskRepository) : IRequestHandler<TaskChangeStatusCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TaskChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        if (!Enum.TryParse<TaskStatusEnum>(request.status, true, out var status))
        {
            return TaskError.StatusNotValid;
        }

        task.SetStatus(status.ToString());

        taskRepository.Update(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}