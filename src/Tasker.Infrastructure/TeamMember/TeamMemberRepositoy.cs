using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.TeamMember;

public class TeamMemberRepositoy
    (TeamTaskerDbContext dbContext) : ITeamMemberRepository
{
    public async Task<TeamMemberModel> GetTeamMemberAsync(Guid userId, Guid teamId)
    {
        return await dbContext.TeamMembers
            .FirstOrDefaultAsync(tm => tm.UserId == userId && tm.TeamId == teamId);
    }

    public async Task<List<UserModel>> GetMembersAsync(Guid teamId)
    {
        var userIds = await dbContext.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .Select(tm => tm.UserId)
            .ToListAsync();

        return await dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();
    }

    public async Task<List<TeamModel>> GetTeamsByMemberAsync(IEnumerable<Guid> teamIds, Guid userId)
    {
        var teamsId = await dbContext.TeamMembers
            .Where(tm => teamIds.Contains(tm.TeamId) && tm.UserId == userId)
            .Select(tm => tm.TeamId)
            .ToListAsync();

        return await dbContext.Teams
            .Where(t => teamsId.Contains(t.Id))
            .ToListAsync();
    }

    public async Task AddAsync(TeamMemberModel model)
    {
        await dbContext.TeamMembers.AddAsync(model);
    }

    public void Delete(TeamMemberModel model)
    {
        dbContext.TeamMembers.Remove(model);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}