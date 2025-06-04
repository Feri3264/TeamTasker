using Tasker.Domain.Session;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<SessionModel> GetByIdAsync(Guid id);

    Task<List<SessionModel>> GetByIdsAsync(IEnumerable<Guid> ids);

    Task AddAsync(SessionModel model);

    void Update(SessionModel model);

    void Delete(SessionModel model);

    Task SaveAsync();
}