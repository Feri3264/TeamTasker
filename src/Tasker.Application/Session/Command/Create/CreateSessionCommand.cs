using ErrorOr;
using MediatR;
using Tasker.Domain.Session;

namespace Tasker.Application.Session.Command.Create;

public record CreateSessionCommand
    (string name , Guid ownerId) : IRequest<ErrorOr<SessionModel>>;