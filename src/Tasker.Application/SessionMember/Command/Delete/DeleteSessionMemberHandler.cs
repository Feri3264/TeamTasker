using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.SessionMember;
using Tasker.Domain.User;

namespace Tasker.Application.SessionMember.Command.Delete;

public class DeleteSessionMemberHandler
    (ISessionMemberRepository sessionMemberRepository,
        ISessionRepository sessionRepository,
        IUserRepository userRepository) : IRequestHandler<DeleteSessionMemberCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteSessionMemberCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);

        if (user is null)
            return UserError.UserNotFound;

        var session = await sessionRepository.GetByIdAsync(request.SessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        var membership =
            await sessionMemberRepository.GetSessionMemberAsync(request.userId, request.SessionId);

        if (membership is null)
            return SessionMemberError.MembershipNotFound;

        session.RemoveSessionMember(membership.Id);
        user.RemoveSessionMember(membership.Id);
        user.RemoveSession(session.Id);

        userRepository.Update(user);
        sessionRepository.Update(session);

        sessionMemberRepository.Delete(membership);
        sessionMemberRepository.SaveAsync();
        return Result.Success;
    }
}