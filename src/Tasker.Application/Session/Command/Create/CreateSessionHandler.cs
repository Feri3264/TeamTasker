using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.User;

namespace Tasker.Application.Session.Command.Create;

public class CreateSessionHandler
    (ISessionRepository sessionRepository,
        IUserRepository userRepository) : IRequestHandler<CreateSessionCommand , ErrorOr<SessionModel>>
{
    public async Task<ErrorOr<SessionModel>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var newSession = SessionModel.Create(request.name , request.ownerId);

        if (newSession.IsError)
            return newSession.Errors;

        var user = await userRepository.GetByIdAsync(request.ownerId);

        if (user is null || user.IsDelete)
            return UserError.UserNotFound;


        await sessionRepository.AddAsync(newSession.Value);
        await sessionRepository.SaveAsync();
        return newSession.Value;
    }
}