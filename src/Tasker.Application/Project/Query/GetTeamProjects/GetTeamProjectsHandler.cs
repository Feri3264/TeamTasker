using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Session;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Query.GetTeamProjects;

public class GetTeamProjectsHandler
    (IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository,
        ITeamRepository teamRepository,
        ISessionRepository sessionRepository) : IRequestHandler<GetTeamProjectsQuery , ErrorOr<List<ProjectModel>>>
{
    public async Task<ErrorOr<List<ProjectModel>>> Handle(GetTeamProjectsQuery request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.teamId);

        if (team is null)
            return TeamError.TeamNotFound;

        var session = await sessionRepository.GetByIdAsync(team.SessionId);

        var projects = new List<ProjectModel>();

        if (session.OwnerId == request.userId || session.Editors.Contains(request.userId) || team.LeadId == request.userId)
        {
            projects = await projectRepository.GetByIdsAsync(team.ProjectIds);
        }
        else
        {
            projects = await projectMemberRepository
                .GetProjectsByMemberAsync(team.ProjectIds, request.userId);
        }
        
        if (projects is null)
            return ProjectError.ProjectNotFound;

        return projects;
    }
}