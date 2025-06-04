using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.GetUserProfile;

public class GetUserProfileHandler
    (IUserRepository userRepository) : IRequestHandler<GetUserProfileQuery , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.id);

        if (user is null || user.IsDelete)
            return UserError.UserNotFound;

        return user;
    }
}