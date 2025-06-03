using ErrorOr;
using MediatR;

namespace Tasker.Application.User.Command.Delete;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Success>>;