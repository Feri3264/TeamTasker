using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.Login;

public class LoginHandler
    (IUserRepository userRepository) : IRequestHandler<LoginCommand , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.email);
        if (user is null)
            return UserError.EmailOrPasswordNotCorrect;

        if (request.password != user.Password)
            return UserError.EmailOrPasswordNotCorrect;

        return user;
    }
}