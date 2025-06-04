using ErrorOr;
using MediatR;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Query.GetSessionTeams;

public record GetSessionTeamsQuery(Guid sessionId) : IRequest<ErrorOr<List<TeamModel>>>;