using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Application.Team.Command.Create;
using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Command.Create;

public class CreateProjectHandler
    (IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, ErrorOr<ProjectModel>>
{
    public async Task<ErrorOr<ProjectModel>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var newProject = ProjectModel.Create(request.name, request.teamId , request.leadId);

        if (newProject.IsError)
            return newProject.Errors;

        await projectRepository.AddAsync(newProject.Value);
        await projectRepository.SaveAsync();
        return newProject.Value;
    }
}