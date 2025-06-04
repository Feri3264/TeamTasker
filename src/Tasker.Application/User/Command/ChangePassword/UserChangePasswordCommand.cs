using ErrorOr;
using MediatR;

namespace Tasker.Application.User.Command.ChangePassword;

public record UserChangePasswordCommand
    (Guid id ,
        string oldPassword,
        string newPassword) : IRequest<ErrorOr<Success>>;