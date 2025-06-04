using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Query.GetTeamProjects;

public class GetTeamProjectsHandler
    (IProjectRepository projectRepository,
        ITeamRepository teamRepository) : IRequestHandler<GetTeamProjectsQuery , ErrorOr<List<ProjectModel>>>
{
    public async Task<ErrorOr<List<ProjectModel>>> Handle(GetTeamProjectsQuery request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.teamId);

        if (team is null)
            return TeamError.TeamNotFound;

        var projects = await projectRepository.GetByIdsAsync(team.ProjectIds);

        if (projects is null)
            return ProjectError.ProjectNotFound;

        return projects;
    }
}