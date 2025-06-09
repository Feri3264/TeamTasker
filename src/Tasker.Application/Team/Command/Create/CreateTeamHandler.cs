using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Command.Create;

public class CreateTeamHandler
    (ITeamRepository teamRepository,
        ISessionRepository sessionRepository) : IRequestHandler<CreateTeamCommand , ErrorOr<TeamModel>>
{
    public async Task<ErrorOr<TeamModel>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var newTeam = TeamModel.Create(request.name, request.sessionId , request.leadId);

        if (newTeam.IsError)
            return newTeam.Errors;

        var session = await sessionRepository.GetByIdAsync(request.sessionId);

        if (session is null)
            return SessionError.SessionNotFound;

        session.AddTeam(newTeam.Value.Id);
        sessionRepository.Update(session);

        await teamRepository.AddAsync(newTeam.Value);
        await teamRepository.SaveAsync();
        return newTeam.Value;
    }
}