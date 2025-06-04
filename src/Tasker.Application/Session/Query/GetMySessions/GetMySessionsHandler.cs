using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Domain.Session;
using Tasker.Domain.User;

namespace Tasker.Application.Session.Query.GetMySessions
{
    class GetMySessionsHandler
        (IUserRepository userRepository,
            ISessionRepository sessionRepository) : IRequestHandler<GetMySessionsQuery , ErrorOr<List<SessionModel>>>
    {
        public async Task<ErrorOr<List<SessionModel>>> Handle(GetMySessionsQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.userId);

            if (user is null || user.IsDelete)
                return UserError.UserNotFound;

            var sessions = await sessionRepository.GetByIdsAsync(user.SessionIds);

            if (sessions is null)
                return SessionError.SessionNotFound;

            return sessions;
        }
    }
}


//public async Task<List<Project>> GetProjectsByIdsAsync(IEnumerable<Guid> ids)
//{
//    return await _dbContext.Projects
//        .Where(p => ids.Contains(p.Id))
//        .ToListAsync();
//}

