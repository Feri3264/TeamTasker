using ErrorOr;
using MediatR;
using Tasker.Domain.User;

namespace Tasker.Application.ProjectMember.Query.GetProjectMembers;

public record GetProjectMembersQuery(Guid projectId) : IRequest<ErrorOr<List<UserModel>>>;