using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Session
{
    public class SessionError
    {
        public Error SessionNotFound = Error.NotFound
            (code: "session.not.found", description: "Session Not Found");
    }
}
