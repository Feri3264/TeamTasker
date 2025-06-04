using ErrorOr;
using MediatR;

namespace Tasker.Application.Team.Command.ChangeName;

public record TeamChangeNameCommand
    (Guid id , string name) : IRequest<ErrorOr<Success>>;