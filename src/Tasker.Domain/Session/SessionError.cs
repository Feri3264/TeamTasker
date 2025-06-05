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
        public static Error SessionNotFound = Error.NotFound
            (code: "session.not.found", description: "Session Not Found");

        public static Error NameNotValid = Error.Validation
            (code: "name.not.valid", description: "Entered Name is Not Valid");

        public static Error TeamAlreadyExists = Error.Validation
            (code: "team.already.exists", description: "Team Already Exists in The Session");

        public static Error TeamNotExists = Error.NotFound
            (code: "team.not.exists", description: "Session Doesn't Have This Team");


        public static Error EditorAlreadyExists = Error.Validation
            (code: "editor.already.exists", description: "Editor Already Exists in The Session");

        public static Error EditorNotExists = Error.NotFound
            (code: "editor.not.exists", description: "Session Doesn't Have This Editor");
    }
}
