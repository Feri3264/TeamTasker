using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.GetUserByRefreshToken;

public record GetUserByRefreshTokenQuery(string refreshToken) : IRequest<ErrorOr<UserModel>>;