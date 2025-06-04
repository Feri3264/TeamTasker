using ErrorOr;
using MediatR;

namespace Tasker.Application.SessionMember.Command.Delete;

public record DeleteSessionMemberCommand
    (Guid userId , Guid SessionId) : IRequest<ErrorOr<Success>>;