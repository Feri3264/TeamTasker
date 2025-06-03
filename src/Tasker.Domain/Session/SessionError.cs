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

        public static Error NameNotValid = Error.Validation
            (code: "name.not.valid", description: "Entered Name is Not Valid");

        public static Error SessionMemberAlreadyExists = Error.Validation
            (code: "sessionMember.already.exists", description: "Session Already Has This User");

        public static Error SessionMemberNotExists = Error.NotFound
            (code: "sessionMember.not.exists", description: "Session Doesn't Have This User");

        public static Error TeamAlreadyExists = Error.Validation
            (code: "team.already.exists", description: "Team Already Exists in The Session");

        public static Error TeamNotExists = Error.NotFound
            (code: "team.not.exists", description: "Session Doesn't Have This Team");
    }
}
