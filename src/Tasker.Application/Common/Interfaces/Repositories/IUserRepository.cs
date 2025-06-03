using Tasker.Domain.User;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserModel> GetByIdAsync(Guid id);

    Task<bool> IsEmailExists(string email);

    Task AddAsync(UserModel model);

    Task SavaAsync();
}