using Tasker.Domain.Team;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface ITeamRepository
{
    Task<TeamModel> GetByIdAsync(Guid id);

    Task AddAsync(TeamModel model);

    void Delete(TeamModel model);

    Task SaveAsync();
}