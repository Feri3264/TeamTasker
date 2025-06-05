using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Auth;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.ChangePassword;

public class UserChangePasswordHandler
    (IUserRepository userRepository,
        IPasswordService passwordService) : IRequestHandler<UserChangePasswordCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.id);

        if (user is null || user.IsDelete)
            return UserError.UserNotFound;

        if (user.Password != passwordService.HashPassword(request.oldPassword))
            return UserError.oldPasswordNotCorrect;

        user.SetPassword(passwordService.HashPassword(request.newPassword));

        userRepository.Update(user);
        await userRepository.SavaAsync();

        return Result.Success;
    }
}