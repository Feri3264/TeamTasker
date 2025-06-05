using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Application.TeamMember.Command.Create;

public class CreateTeamMemberHandler
    (IUserRepository userRepository,
        ITeamRepository teamRepository,
        ITeamMemberRepository teamMemberRepository) : IRequestHandler<CreateTeamMemberCommand , ErrorOr<TeamMemberModel>>
{
    public async Task<ErrorOr<TeamMemberModel>> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);

        if (user is null)
            return UserError.UserNotFound;

        var team = await teamRepository.GetByIdAsync(request.teamId);

        if (team is null)
            return TeamError.TeamNotFound;

        var newMembership = new TeamMemberModel
            (request.userId,
                request.teamId,
                team.LeadId == request.userId ? true : false);

        await teamMemberRepository.AddAsync(newMembership);
        await teamMemberRepository.SaveAsync();
        return newMembership;
    }
}