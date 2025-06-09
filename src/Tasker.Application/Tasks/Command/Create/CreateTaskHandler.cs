using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.Create;

public class CreateTaskHandler
    (ITaskRepository taskRepository) : IRequestHandler<CreateTaskCommand, ErrorOr<TaskModel>>
{
    public async Task<ErrorOr<TaskModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<TaskStatusEnum>(request.status, true, out var status))
        {
            return TaskError.StatusNotValid;
        }

        if (!Enum.TryParse<TaskPriorityEnum>(request.priority, true, out var priority))
        {
            return TaskError.PriorityNotValid;
        }

        var newTask = TaskModel.Create(
            request.name,
            status.ToString(),
            priority.ToString(),
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