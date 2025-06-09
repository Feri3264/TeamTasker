using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;
using Tasker.Domain.User;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.Create;

public class CreateTaskHandler
    (ITaskRepository taskRepository,
        IProjectRepository projectRepository,
        IUserRepository userRepository) : IRequestHandler<CreateTaskCommand, ErrorOr<TaskModel>>
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


        var project = await projectRepository.GetByIdAsync(request.projectId);

        if (project is null)
            return ProjectError.ProjectNotFound;


        var user = await userRepository.GetByIdAsync(request.assignedMemberId);

        if (user is null || user.IsDelete)
            return UserError.UserNotFound;


        project.AddTask(newTask.Value.Id);
        projectRepository.Update(project);

        user.AddTask(newTask.Value.Id);
        userRepository.Update(user);

        await taskRepository.AddAsync(newTask.Value);
        await taskRepository.SaveAsync();
        return newTask.Value;
    }
}