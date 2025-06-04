using Tasker.Domain.Team;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ITeamRepository
{
    Task<TeamModel> GetByIdAsync(Guid id);

    Task<List<TeamModel>> GetByIdsAsync(IEnumerable<Guid> ids);

    Task AddAsync(TeamModel model);

    void Update(TeamModel model);

    void Delete(TeamModel model);

    Task SaveAsync();
}