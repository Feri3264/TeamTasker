using ErrorOr;
using MediatR;

namespace Tasker.Application.Team.Command.Delete;

public record DeleteTeamCommand(Guid id) : IRequest<ErrorOr<Success>>;