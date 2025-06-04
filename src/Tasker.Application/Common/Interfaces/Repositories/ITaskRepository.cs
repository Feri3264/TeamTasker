using Tasker.Domain.Project;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ITaskRepository
{
    Task<TaskModel> GetByIdAsync(Guid id);

    Task<List<TaskModel>> GetByIdsAsync(IEnumerable<Guid> ids);

    Task AddAsync(TaskModel model);

    void Update(TaskModel model);

    void Delete(TaskModel model);

    Task SaveAsync();
}