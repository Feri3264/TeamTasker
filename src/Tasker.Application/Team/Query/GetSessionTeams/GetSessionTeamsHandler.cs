using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Query.GetSessionTeams;

public class GetSessionTeamsHandler
    (ISessionRepository sessionRepository,
        ITeamRepository teamRepository,
        ITeamMemberRepository teamMemberRepository) : IRequestHandler<GetSessionTeamsQuery , ErrorOr<List<TeamModel>>>
{
    public async Task<ErrorOr<List<TeamModel>>> Handle(GetSessionTeamsQuery request, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.GetByIdAsync(request.sessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        var teams = new List<TeamModel>();

        if (session.OwnerId == request.userId || session.Editors.Contains(request.userId))
        {
            teams = await teamRepository.GetByIdsAsync(session.TeamIds);
        }
        else
        {
            teams = await teamMemberRepository
                .GetTeamByMemberAsync(session.TeamIds, request.userId);
        }

        if (teams is null)
            return TeamError.TeamNotFound;

        return teams;
    }
}