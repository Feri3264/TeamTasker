using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.SessionMember.Query.GetMembers;

public record GetSessionMembersCommand(Guid sessionId) : IRequest<ErrorOr<List<UserModel>>>;