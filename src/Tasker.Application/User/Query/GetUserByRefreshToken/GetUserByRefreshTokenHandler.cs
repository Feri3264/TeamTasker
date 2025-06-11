using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.GetUserByRefreshToken;

public class GetUserByRefreshTokenHandler
    (IUserRepository userRepository) : IRequestHandler<GetUserByRefreshTokenQuery , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByRefreshToken(request.refreshToken);

        if (user is null)
            return UserError.UserNotFound;

        return user;
    }
}