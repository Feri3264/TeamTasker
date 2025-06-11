using Microsoft.EntityFrameworkCore;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.User;

public class UserRepository
    (TeamTaskerDbContext dbContext) : IUserRepository
{
    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserModel> GetByEmailAsync(string email)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserModel> GetUserByRefreshToken(string token)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == token);
    }

    public async Task<bool> IsEmailExists(string email)
    {
        return await dbContext.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(UserModel model)
    {
        await dbContext.Users.AddAsync(model);
    }

    public void Update(UserModel model)
    {
        dbContext.Users.Update(model);
    }

    public async Task SavaAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}