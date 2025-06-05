using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.ProjectMember.Command.Create;

public class CreateProjectMemberHandler
    (IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository,
        IUserRepository userRepository) : IRequestHandler<CreateProjectMemberCommand , ErrorOr<ProjectMemberModel>>
{
    public async Task<ErrorOr<ProjectMemberModel>> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);

        if (user is null)
            return UserError.UserNotFound;

        var project = await projectRepository.GetByIdAsync(request.projectId);

        if (project is null)
            return ProjectError.ProjectNotFound;

        var newMembership = new ProjectMemberModel
        (request.userId,
            request.projectId,
            project.LeadId == request.userId ? true : false);

        await projectMemberRepository.AddAsync(newMembership);
        await projectMemberRepository.SaveAsync();
        return newMembership;
    }
}