using ErrorOr;
using MediatR;
using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Command.Create;

public record CreateProjectCommand
    (string name, Guid teamId , Guid leadId) : IRequest<ErrorOr<ProjectModel>>;