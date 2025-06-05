using ErrorOr;
using MediatR;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.TeamMember;

namespace Tasker.Application.ProjectMember.Command.Create;

public record CreateProjectMemberCommand
    (Guid projectId, Guid userId) : IRequest<ErrorOr<ProjectMemberModel>>;