using ErrorOr;
using MediatR;
using Tasker.Domain.Project;

namespace Tasker.Application.Project.Query.GetTeamProjects;

public record GetTeamProjectsQuery(Guid teamId) : IRequest<ErrorOr<List<ProjectModel>>>;