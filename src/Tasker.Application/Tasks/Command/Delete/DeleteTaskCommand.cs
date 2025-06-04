using ErrorOr;
using MediatR;

namespace Tasker.Application.Tasks.Command.Delete;

public record DeleteTaskCommand(Guid id) : IRequest<ErrorOr<Success>>;