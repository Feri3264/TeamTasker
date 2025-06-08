using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.SessionMember.Query.GetSessionMembers;

public record GetSessionMembersQuery(Guid sessionId) : IRequest<ErrorOr<List<UserModel>>>;