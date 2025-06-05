using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.TeamMember.Query.GetTeamMembers;

public record GetTeamMembersQuery(Guid teamId) : IRequest<ErrorOr<List<UserModel>>>;