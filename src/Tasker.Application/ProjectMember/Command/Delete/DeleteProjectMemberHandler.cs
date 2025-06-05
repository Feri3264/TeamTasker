using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.ProjectMember.Command.Delete;

public class DeleteProjectMemberHandler
    (IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository,
        IUserRepository userRepository) : IRequestHandler<DeleteProjectMemberCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);

        if (user is null)
            return UserError.UserNotFound;

        var project = await projectRepository.GetByIdAsync(request.projectId);

        if (project is null)
            return ProjectError.ProjectNotFound;

        var membership = await projectMemberRepository.GetProjectMemberAsync(request.userId, request.projectId);

        if (membership is null)
            return ProjectMemberError.MembershipNotFound;

        projectMemberRepository.Delete(membership);
        await projectMemberRepository.SaveAsync();
        return Result.Success;
    }
}