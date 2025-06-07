using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.User;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.ProjectMember;

public class ProjectMemberRepository
    (TeamTaskerDbContext dbContext) : IProjectMemberRepository
{
    public async Task<ProjectMemberModel> GetProjectMemberAsync(Guid userId, Guid projectId)
    {
        return await dbContext.ProjectMembers
            .FirstOrDefaultAsync(p => p.UserId == userId && p.ProjectId == projectId);
    }

    public async Task<List<UserModel>> GetMembersAsync(Guid projectId)
    {
        var userIds = await dbContext.ProjectMembers
            .Where(p => p.ProjectId == projectId)
            .Select(p => p.UserId)
            .ToListAsync();

        return await dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();
    }

    public async Task<List<ProjectModel>> GetProjectsByMemberAsync(IEnumerable<Guid> projectIds, Guid userId)
    {
        var projectsId = await dbContext.ProjectMembers
            .Where(pm => projectIds.Contains(pm.ProjectId) && pm.UserId == userId)
            .Select(pm => pm.ProjectId)
            .ToListAsync();

        return await dbContext.Projects
            .Where(p => projectsId.Contains(p.Id))
            .ToListAsync();
    }

    public async Task AddAsync(ProjectMemberModel model)
    {
        await dbContext.ProjectMembers.AddAsync(model);
    }

    public void Delete(ProjectMemberModel model)
    {
        dbContext.ProjectMembers.Remove(model);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}