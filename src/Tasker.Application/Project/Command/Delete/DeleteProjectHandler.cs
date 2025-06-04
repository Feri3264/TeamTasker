using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Project.Command.Delete;

public class DeleteProjectHandler
    (IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.id);

        if (project is null)
            return ProjectError.ProjectNotFound;

        projectRepository.Delete(project);
        await projectRepository.SaveAsync();
        return Result.Success;
    }
}