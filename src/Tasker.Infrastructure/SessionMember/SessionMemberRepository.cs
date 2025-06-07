using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.SessionMember;
using Tasker.Domain.User;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.SessionMember;

public class SessionMemberRepository
    (TeamTaskerDbContext dbContext) : ISessionMemberRepository
{
    public async Task<SessionMemberModel> GetSessionMemberAsync(Guid userId, Guid sessionId)
    {
        return await dbContext.SessionMembers
            .FirstOrDefaultAsync(sm => sm.UserId == userId && sm.SessionId == sessionId);
    }

    public async Task<List<UserModel>> GetMembersAsync(Guid sessionId)
    {
        var userIds =  await dbContext.SessionMembers
            .Where(sm => sm.SessionId == sessionId)
            .Select(sm => sm.UserId)
            .ToListAsync();

        return await dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();
    }

    public async Task AddAsync(SessionMemberModel model)
    {
        dbContext.SessionMembers.AddAsync(model);
    }

    public void Delete(SessionMemberModel model)
    {
        dbContext.SessionMembers.Remove(model);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}