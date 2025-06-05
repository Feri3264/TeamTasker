using Tasker.Domain.SessionMember;
using Tasker.Domain.User;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ISessionMemberRepository
{
    Task<SessionMemberModel> GetSessionMemberAsync(Guid useId, Guid sessionId);

    Task<List<UserModel>> GetMembersAsync(Guid sessionId);

    Task AddAsync(SessionMemberModel model);

    void Delete(SessionMemberModel model);

    Task SaveAsync();
}