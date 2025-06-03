using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.Register;

public class RegisterHandler
    (IUserRepository userRepository) : IRequestHandler<RegisterCommand , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = UserModel.Create(request.name, request.email, request.password);

        if (newUser.IsError)
            return newUser.Errors;

        if (await userRepository.IsEmailExists(request.email))
            return UserError.EmailAlreadyTaken;

        await userRepository.AddAsync(newUser.Value);
        await userRepository.SavaAsync();
        return newUser.Value;
    }
}