using ErrorOr;
using MediatR;

namespace Tasker.Application.TeamMember.Command.Delete;

public record DeleteTeamMemberCommand
    (Guid teamId , Guid userId) : IRequest<ErrorOr<Success>>;