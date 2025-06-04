using ErrorOr;
using MediatR;
using Tasker.Domain.SessionMember;

namespace Tasker.Application.SessionMember.Command.Create;

public record CreateSessionMemberCommand
    (Guid sessionId , Guid userId) : IRequest<ErrorOr<SessionMemberModel>>;