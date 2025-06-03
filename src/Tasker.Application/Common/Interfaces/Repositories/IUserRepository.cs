using Tasker.Domain.User;

namespace Tasker.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserModel> GetByIdAsync(Guid id);

    Task<UserModel> GetByEmailAsync(string email);

    Task<bool> IsEmailExists(string email);

    Task AddAsync(UserModel model);

    void Update(UserModel model);

    Task SavaAsync();
}