using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.GetMyProfile;

public class GetMyProfileHandler
    (IUserRepository userRepository) : IRequestHandler<GetMyProfileQuery , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.id);

        if (user is null || user.IsDelete)
            return UserError.UserNotFound;

        return user;
    }
}