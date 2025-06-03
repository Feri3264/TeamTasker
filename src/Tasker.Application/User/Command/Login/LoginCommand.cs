using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.Login;

public record LoginCommand
    (string email , string password) : IRequest<ErrorOr<UserModel>>;