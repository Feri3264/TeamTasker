using ErrorOr;
using MediatR;
using Tasker.Domain.Project;

namespace Tasker.Application.Project.Query.GetTeamProjects;

public record GetTeamProjectsQuery(Guid teamId , Guid userId) : IRequest<ErrorOr<List<ProjectModel>>>;