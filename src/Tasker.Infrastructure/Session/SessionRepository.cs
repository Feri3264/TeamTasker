using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.Session;

public class SessionRepository
    (TeamTaskerDbContext dbContext) : ISessionRepository
{
    public async Task<SessionModel> GetByIdAsync(Guid id)
    {
        return await dbContext.Sessions
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<SessionModel>> GetUserSessions(Guid userId)
    {
        var sessionIds = await dbContext.SessionMembers
            .Where(sm => sm.UserId == userId)
            .Select(sm => sm.SessionId)
            .ToListAsync();

        var sessions = await dbContext.Sessions
            .Where(s => sessionIds.Contains(s.Id))
            .ToListAsync();
        return sessions;
    }

    public async Task AddAsync(SessionModel model)
    {
        await dbContext.Sessions.AddAsync(model);
    }

    public void Update(SessionModel model)
    {
        dbContext.Sessions.Update(model);
    }

    public void Delete(SessionModel model)
    {
        dbContext.Sessions.Remove(model);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}