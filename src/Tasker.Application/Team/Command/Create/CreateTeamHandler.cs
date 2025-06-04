using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Command.Create;

public class CreateTeamHandler
    (ITeamRepository teamRepository) : IRequestHandler<CreateTeamCommand , ErrorOr<TeamModel>>
{
    public async Task<ErrorOr<TeamModel>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var newTeam = TeamModel.Create(request.name, request.sessionId);

        if (newTeam.IsError)
            return newTeam.Errors;

        await teamRepository.AddAsync(newTeam.Value);
        await teamRepository.SaveAsync();
        return newTeam.Value;
    }
}