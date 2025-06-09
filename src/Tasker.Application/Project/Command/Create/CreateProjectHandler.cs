using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Application.Team.Command.Create;
using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Command.Create;

public class CreateProjectHandler
    (IProjectRepository projectRepository,
        ITeamRepository teamRepository) : IRequestHandler<CreateProjectCommand, ErrorOr<ProjectModel>>
{
    public async Task<ErrorOr<ProjectModel>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var newProject = ProjectModel.Create(request.name, request.teamId , request.leadId);

        if (newProject.IsError)
            return newProject.Errors;

        var team = await teamRepository.GetByIdAsync(newProject.Value.TeamId);

        if (team is null)
            return TeamError.TeamNotFound;

        team.AddProject(newProject.Value.Id);
        teamRepository.Update(team);

        await projectRepository.AddAsync(newProject.Value);
        await projectRepository.SaveAsync();
        return newProject.Value;
    }
}