using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.TeamMember.Command.Delete;

public class DeleteTeamMemberHandler
    (ITeamRepository teamRepository,
        ITeamMemberRepository teamMemberRepository,
        IUserRepository userRepository) : IRequestHandler<DeleteTeamMemberCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);

        if (user is null)
            return UserError.UserNotFound;

        var team = await teamRepository.GetByIdAsync(request.teamId);

        if (team is null)
            return TeamError.TeamNotFound;

        var membership = await teamMemberRepository.GetTeamMemberAsync(request.userId, request.teamId);

        if (membership is null)
            return TeamMemberError.MembershipNotFound;

        teamMemberRepository.Delete(membership);
        await teamMemberRepository.SaveAsync();
        return Result.Success;
    }
}