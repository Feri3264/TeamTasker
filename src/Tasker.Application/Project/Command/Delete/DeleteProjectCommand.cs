using ErrorOr;
using MediatR;

namespace Tasker.Application.Project.Command.Delete;

public record DeleteProjectCommand(Guid id) : IRequest<ErrorOr<Success>>;