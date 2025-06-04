using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Command.Delete;

public class DeleteTeamHandler
    (ITeamRepository teamRepository) : IRequestHandler<DeleteTeamCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.id);

        if (team is null)
            return TeamError.TeamNotFound;

        teamRepository.Delete(team);
        await teamRepository.SaveAsync();
        return Result.Success;
    }
}