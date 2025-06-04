using ErrorOr;
using MediatR;

namespace Tasker.Application.Session.Command.Delete;

public record DeleteSessionCommand(Guid id) : IRequest<ErrorOr<Success>>;