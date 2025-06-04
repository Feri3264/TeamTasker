using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Command.ChangeName;

public class ProjectChangeNameHandler
    (IProjectRepository projectRepository) : IRequestHandler<ProjectChangeNameCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ProjectChangeNameCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.id);

        if (project is null)
            return ProjectError.ProjectNotFound;

        var nameResult = project.SetName(request.name);

        if (nameResult.IsError)
            return nameResult.Errors;

        projectRepository.Update(project);
        await projectRepository.SaveAsync();
        return Result.Success;
    }
}