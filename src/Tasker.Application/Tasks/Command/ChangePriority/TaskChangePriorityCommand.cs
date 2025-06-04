using ErrorOr;
using MediatR;

namespace Tasker.Application.Tasks.Command.ChangePriority;

public record TaskChangePriorityCommand
    (Guid id , string priority) : IRequest<ErrorOr<Success>>;