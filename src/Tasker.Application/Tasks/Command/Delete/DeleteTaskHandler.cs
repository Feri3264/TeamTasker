using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;
using Tasker.Domain.User;

namespace Tasker.Application.Tasks.Command.Delete;

public class DeleteTaskHandler
    (ITaskRepository taskRepository,
        IUserRepository userRepository) : IRequestHandler<DeleteTaskCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        var user = await userRepository.GetByIdAsync(task.AssignedMemberId);

        if (user is null)
            return UserError.UserNotFound;

        user.RemoveTask(task.Id);
        userRepository.Update(user);

        taskRepository.Delete(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}