using Tasker.Domain.SessionMember;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ISessionMemberRepository
{
    Task<SessionMemberModel> GetByIdAsync(Guid id);

    Task AddAsync(SessionMemberModel model);

    Task SaveAsync();
}