using ErrorOr;
using MediatR;

namespace Tasker.Application.ProjectMember.Command.Delete;

public record DeleteProjectMemberCommand
    (Guid projectId , Guid userId) : IRequest<ErrorOr<Success>>;