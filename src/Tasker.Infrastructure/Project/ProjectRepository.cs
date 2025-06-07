using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.Project;

public class ProjectRepository
    (TeamTaskerDbContext dbContext) : IProjectRepository
{
    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        return await dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<ProjectModel>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await dbContext.Projects
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }

    public async Task AddAsync(ProjectModel model)
    {
        await dbContext.Projects.AddAsync(model);
    }

    public void Update(ProjectModel model)
    {
        dbContext.Projects.Update(model);
    }

    public void Delete(ProjectModel model)
    {
        dbContext.Projects.Remove(model);
    }

    public async Task SaveAsync()
    {
        dbContext.SaveChangesAsync();

    }
}