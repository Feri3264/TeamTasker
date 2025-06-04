using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Command.Create;

public class CreateTaskHandler
    (ITaskRepository taskRepository) : IRequestHandler<CreateTaskCommand, ErrorOr<TaskModel>>
{
    public async Task<ErrorOr<TaskModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var newTask = TaskModel.Create(
            request.name,
            request.status,
            request.priority,
            request.assignedMemberId,
            request.projectId,
            request.deadline);

        if (newTask.IsError)
            return newTask.Errors;

        await taskRepository.AddAsync(newTask.Value);
        await taskRepository.SaveAsync();
        return newTask.Value;
    }
}