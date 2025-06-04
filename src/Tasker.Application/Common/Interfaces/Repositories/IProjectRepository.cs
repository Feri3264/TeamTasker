using Tasker.Domain.Project;
using Tasker.Domain.Team;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<ProjectModel> GetByIdAsync(Guid id);

    Task<List<ProjectModel>> GetByIdsAsync(IEnumerable<Guid> ids);

    Task AddAsync(ProjectModel model);

    void Update(ProjectModel model);

    void Delete(ProjectModel model);

    Task SaveAsync();
}