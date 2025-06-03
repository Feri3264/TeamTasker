using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.SearchUserByEmail;

public record SearchUserByEmailQuery(string email) : IRequest<ErrorOr<UserModel>>;