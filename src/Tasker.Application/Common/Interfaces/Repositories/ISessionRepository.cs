using System.Collections.Specialized;
using Tasker.Domain.Session;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<SessionModel> GetByIdAsync(Guid id);

    Task<List<SessionModel>> GetUserSessions(Guid userId);

    Task AddAsync(SessionModel model);

    void Update(SessionModel model);

    void Delete(SessionModel model);

    Task SaveAsync();
}