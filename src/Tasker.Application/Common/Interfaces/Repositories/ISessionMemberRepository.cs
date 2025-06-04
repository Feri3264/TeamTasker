using Tasker.Domain.SessionMember;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ISessionMemberRepository
{
    Task<SessionMemberModel> GetSessionMemberAsync(Guid useId, Guid sessionId);

    Task AddAsync(SessionMemberModel model);

    void Delete(SessionMemberModel model);

    Task SaveAsync();
}