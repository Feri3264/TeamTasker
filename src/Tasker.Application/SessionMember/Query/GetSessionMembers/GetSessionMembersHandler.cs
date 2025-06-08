using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.SessionMember;
using Tasker.Domain.User;

namespace Tasker.Application.SessionMember.Query.GetSessionMembers;

public class GetSessionMembersHandler
    (ISessionRepository sessionRepository,
        ISessionMemberRepository sessionMemberRepository) : IRequestHandler<GetSessionMembersQuery , ErrorOr<List<UserModel>>>
{
    public async Task<ErrorOr<List<UserModel>>> Handle(GetSessionMembersQuery request, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.GetByIdAsync(request.sessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        var members = await sessionMemberRepository.GetMembersAsync(request.sessionId);

        if (members is null)
            return SessionMemberError.MemberNotFound;

        return members;
    }
}