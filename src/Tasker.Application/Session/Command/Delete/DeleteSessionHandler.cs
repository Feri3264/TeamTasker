using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;

namespace Tasker.Application.Session.Command.Delete;

public class DeleteSessionHandler
    (ISessionRepository sessionRepository) : IRequestHandler<DeleteSessionCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.GetByIdAsync(request.id);

        if (session is null)
            return SessionError.SessionNotFound;

        sessionRepository.Delete(session);
        await sessionRepository.SaveAsync();

        return Result.Success;
    }
}