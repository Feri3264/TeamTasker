using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.User.Query.GetMyProfile;

public record GetMyProfileQuery(Guid id) : IRequest<ErrorOr<UserModel>>;