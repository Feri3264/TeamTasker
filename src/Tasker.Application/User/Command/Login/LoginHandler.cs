using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Auth;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.Login;

public class LoginHandler
    (IUserRepository userRepository,
        IPasswordService passwordService,
        IRefreshTokenService refreshTokenService) : IRequestHandler<LoginCommand , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.email);
        if (user is null)
            return UserError.EmailOrPasswordNotCorrect;

        if (user.IsDelete)
            return UserError.UserAccountDeleted;

        if (passwordService.HashPassword(request.password) != user.Password)
            return UserError.EmailOrPasswordNotCorrect;

        var newRefreshToken = refreshTokenService.GenerateRefreshToken();
        user.SetRefreshToken(newRefreshToken);
        user.SetTokenExpireTime(DateTime.UtcNow.AddDays(10));

        userRepository.Update(user);
        await userRepository.SavaAsync();
        return user;
    }
}