using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Tasks;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.Tasks;

public class TaskRepository
    (TeamTaskerDbContext dbContext) : ITaskRepository
{
    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        return await dbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TaskModel>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await dbContext.Tasks
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();
    }

    public async Task AddAsync(TaskModel model)
    {
        await dbContext.Tasks.AddAsync(model);
    }

    public void Update(TaskModel model)
    {
        dbContext.Tasks.Update(model);
    }

    public void Delete(TaskModel model)
    {
        dbContext.Tasks.Remove(model);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}