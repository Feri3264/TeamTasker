using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Command.ChangeName;

public class TaskChangeNameHandler
    (ITaskRepository taskRepository) : IRequestHandler<TaskChangeNameCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TaskChangeNameCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        var nameResult = task.SetName(request.name);

        if (nameResult.IsError)
            return nameResult.Errors;

        taskRepository.Update(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}