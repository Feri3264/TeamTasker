using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.Team;

public class TeamRepository
    (TeamTaskerDbContext dbContext) : ITeamRepository
{
    public async Task<TeamModel> GetByIdAsync(Guid id)
    {
        return await dbContext.Teams
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TeamModel>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await dbContext.Teams
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();
    }

    public async Task AddAsync(TeamModel model)
    {
        await dbContext.Teams.AddAsync(model);
    }

    public void Update(TeamModel model)
    {
        dbContext.Teams.Update(model);
    }

    public void Delete(TeamModel model)
    {
        dbContext.Teams.Update(model);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}