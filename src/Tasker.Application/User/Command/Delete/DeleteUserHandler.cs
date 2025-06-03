using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.Delete;

public class DeleteUserHandler
    (IUserRepository userRepository) : IRequestHandler<DeleteUserCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return UserError.UserNotFound;

        user.Delete();
        userRepository.Update(user);
        await userRepository.SavaAsync();

        return Result.Success;
    }
}