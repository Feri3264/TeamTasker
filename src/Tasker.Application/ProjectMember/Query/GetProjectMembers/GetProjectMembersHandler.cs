using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.ProjectMember.Query.GetProjectMembers;

public class GetProjectMembersHandler
    (IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository) : IRequestHandler<GetProjectMembersQuery , ErrorOr<List<UserModel>>>
{
    public async Task<ErrorOr<List<UserModel>>> Handle(GetProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.projectId);

        if (project is null)
            return ProjectError.ProjectNotFound;

        var members = await projectMemberRepository.GetMembersAsync(request.projectId);

        if (members is null)
            return ProjectMemberError.MemberNotFound;

        return members;
    }
}