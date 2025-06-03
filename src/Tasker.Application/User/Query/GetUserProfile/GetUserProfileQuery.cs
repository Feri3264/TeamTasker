using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.GetUserProfile;

public record GetUserProfileQuery(Guid id) : IRequest<ErrorOr<UserModel>>;