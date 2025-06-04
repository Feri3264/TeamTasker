using ErrorOr;
using MediatR;
using Tasker.Domain.Session;

namespace Tasker.Application.Session.Query.GetMySessions;

public record GetMySessionsQuery(Guid userId) : IRequest<ErrorOr<List<SessionModel>>>;