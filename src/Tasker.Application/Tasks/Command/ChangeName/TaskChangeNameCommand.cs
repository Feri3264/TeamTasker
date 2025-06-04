using ErrorOr;
using MediatR;

namespace Tasker.Application.Tasks.Command.ChangeName;

public record TaskChangeNameCommand
    (Guid id, string name) : IRequest<ErrorOr<Success>>;