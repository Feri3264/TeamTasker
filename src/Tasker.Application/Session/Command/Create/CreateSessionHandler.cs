using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;

namespace Tasker.Application.Session.Command.Create;

public class CreateSessionHandler
    (ISessionRepository sessionRepository) : IRequestHandler<CreateSessionCommand , ErrorOr<SessionModel>>
{
    public async Task<ErrorOr<SessionModel>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var newSession = SessionModel.Create(request.name , request.ownerId);

        if (newSession.IsError)
            return newSession.Errors;

        await sessionRepository.AddAsync(newSession.Value);
        await sessionRepository.SaveAsync();
        return newSession.Value;
    }
}