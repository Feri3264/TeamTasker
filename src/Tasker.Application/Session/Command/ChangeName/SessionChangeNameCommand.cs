using ErrorOr;
using MediatR;

namespace Tasker.Application.Session.Command.ChangeName;

public record SessionChangeNameCommand
    (Guid id, string name) : IRequest<ErrorOr<Success>>;