using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Command.ChangeName;

public class TeamChangeNameHandler
    (ITeamRepository teamRepository) : IRequestHandler<TeamChangeNameCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(TeamChangeNameCommand request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.id);

        if (team is null)
            return TeamError.TeamNotFound;

        var nameResult = team.SetName(request.name);

        if (nameResult.IsError)
            return nameResult.Errors;

        teamRepository.Update(team);
        await teamRepository.SaveAsync();
        return Result.Success;
    }
}