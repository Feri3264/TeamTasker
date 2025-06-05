using Tasker.Domain.SessionMember;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ITeamMemberRepository
{
    Task<TeamMemberModel> GetTeamMemberAsync(Guid useId, Guid teamId);

    Task<List<UserModel>> GetMembersAsync(Guid teamId);

    Task<List<TeamModel>> GetTeamByMemberAsync(IEnumerable<Guid> teamIds, Guid userId);

    Task AddAsync(TeamMemberModel model);

    void Delete(TeamMemberModel model);

    Task SaveAsync();
}