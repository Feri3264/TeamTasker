using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.User.Command.Register;

public record RegisterCommand
    (string name, string email, string password) : IRequest<ErrorOr<UserModel>>;