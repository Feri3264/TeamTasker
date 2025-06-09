using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Command.Delete;

public class DeleteTeamHandler
    (ITeamRepository teamRepository,
        ISessionRepository sessionRepository) : IRequestHandler<DeleteTeamCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByIdAsync(request.id);

        if (team is null)
            return TeamError.TeamNotFound;

        var session = await sessionRepository.GetByIdAsync(team.SessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        session.RemoveTeam(team.Id);
        sessionRepository.Update(session);

        teamRepository.Delete(team);
        await teamRepository.SaveAsync();
        return Result.Success;
    }
}