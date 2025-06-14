﻿using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;
using Tasker.Domain.User;

namespace Tasker.Application.Tasks.Command.ChangeMember;

public class TaskChangeMemberHandler
    (ITaskRepository taskRepository,
        IUserRepository userRepository) : IRequestHandler<TaskChangeMemberCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TaskChangeMemberCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.id);

        if (task is null)
            return TaskError.TaskNotFound;

        var preUser = await userRepository.GetByIdAsync(task.AssignedMemberId);

        if (preUser is null)
            return UserError.UserNotFound;


        var user = await userRepository.GetByIdAsync(request.userId);

        if (user is null)
            return UserError.UserNotFound;

        task.ChangeAssignedMember(user.Id);

        user.AddTask(task.Id);
        userRepository.Update(user);

        preUser.RemoveTask(task.Id);
        userRepository.Update(preUser);

        taskRepository.Update(task);
        await taskRepository.SaveAsync();
        return Result.Success;
    }
}