using ErrorOr;
using MediatR;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Query.GetSessionTeams;

public record GetSessionTeamsQuery(Guid sessionId , Guid userId) : IRequest<ErrorOr<List<TeamModel>>>;