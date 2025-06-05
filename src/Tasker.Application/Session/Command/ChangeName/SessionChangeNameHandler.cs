using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.Team;

namespace Tasker.Application.Session.Command.ChangeName;

public class SessionChangeNameHandler
    (ISessionRepository sessionRepository) : IRequestHandler<SessionChangeNameCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(SessionChangeNameCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.GetByIdAsync(request.id);

        if (session is null)
            return SessionError.SessionNotFound;

        var nameResult = session.SetName(request.name);

        if (nameResult.IsError)
            return nameResult.Errors;

        sessionRepository.Update(session);
        await sessionRepository.SaveAsync();
        return Result.Success;
    }
}