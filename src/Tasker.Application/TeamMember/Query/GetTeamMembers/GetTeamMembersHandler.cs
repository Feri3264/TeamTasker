using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.TeamMember.Query.GetTeamMembers;

public class GetTeamMembersHandler
    (ITeamRepository teamRepository,
        ITeamMemberRepository teamMemberRepository) : IRequestHandler<GetTeamMembersQuery , ErrorOr<List<UserModel>>>
{
    public async Task<ErrorOr<List<UserModel>>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.teamId);

        if (team is null)
            return TeamError.TeamNotFound;

        var members = await teamMemberRepository.GetMembersAsync(request.teamId);

        if (members is null)
            return TeamMemberError.MemberNotFound;

        return members;
    }
}