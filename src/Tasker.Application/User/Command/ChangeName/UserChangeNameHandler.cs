using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.ChangeName;

public class UserChangeNameHandler
    (IUserRepository userRepository) : IRequestHandler<UserChangeNameCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UserChangeNameCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.id);

        if (user is null || user.IsDelete)
            return UserError.UserNotFound;

        var name = user.SetName(request.name);
        if (name.IsError)
            return name.Errors;

        userRepository.Update(user);
        await userRepository.SavaAsync();

        return Result.Success;
    }
}