using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.SearchUserByEmail;

public class SearchUserByEmailHandler
    (IUserRepository userRepository) : IRequestHandler<SearchUserByEmailQuery , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(SearchUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.email);
        if (user is null || user.IsDelete)
            return UserError.UserNotFound;

        return user;
    }
}