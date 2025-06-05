using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.SessionMember.Query.GetMembers;

public record GetSessionMembersQuery(Guid sessionId) : IRequest<ErrorOr<List<UserModel>>>;