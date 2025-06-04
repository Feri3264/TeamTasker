using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Command.Delete;

public class DeleteTaskHandler
    (ITaskRepository taskRepository) : IRequestHandler<DeleteTaskCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        taskRepository.Delete(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}