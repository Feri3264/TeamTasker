using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.SessionMember;
using Tasker.Domain.User;

namespace Tasker.Application.SessionMember.Command.Create;

public class CreateSessionMemberHandler
    (ISessionMemberRepository sessionMemberRepository,
        ISessionRepository sessionRepository,
        IUserRepository userRepository) : IRequestHandler<CreateSessionMemberCommand , ErrorOr<SessionMemberModel>>
{
    public async Task<ErrorOr<SessionMemberModel>> Handle(CreateSessionMemberCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);
        if (user is null)
            return UserError.UserNotFound;


        var session = await sessionRepository.GetByIdAsync(request.sessionId);
        if (session is null)
            return SessionError.SessionNotFound;


        var newMember = new SessionMemberModel
            (request.userId,
                request.sessionId,
                session.OwnerId == request.userId ? true : false);


        await sessionMemberRepository.AddAsync(newMember);
        await sessionMemberRepository.SaveAsync();
        return newMember;
    }
}