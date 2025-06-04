using ErrorOr;
using MediatR;

namespace Tasker.Application.Project.Command.ChangeName;

public record ProjectChangeNameCommand
    (Guid id, string name) : IRequest<ErrorOr<Success>>;