using ErrorOr;
using MediatR;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.ChangePriority;

public record TaskChangePriorityCommand
    (Guid id , string priority) : IRequest<ErrorOr<Success>>;