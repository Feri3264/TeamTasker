using Tasker.Domain.ProjectMember;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface IProjectMemberRepository
{
    Task<ProjectMemberModel> GetProjectMemberAsync(Guid useId, Guid projectId);

    Task<List<UserModel>> GetMembersAsync(Guid projectId);

    Task AddAsync(ProjectMemberModel model);

    void Delete(ProjectMemberModel model);

    Task SaveAsync();
}