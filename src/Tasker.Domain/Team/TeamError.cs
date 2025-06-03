using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Team
{
    public class TeamError
    {
        public static Error TeamNotFound = Error.NotFound
            (code: "team.not.found" , description: "Team Not Found");

        public static Error NameNotValid = Error.Validation
            (code: "name.not.valid", description: "Entered Name is Not Valid");

        public static Error TeamMemberAlreadyExists = Error.Validation
            (code: "teamMember.already.exists", description: "Team Already Has This User");

        public static Error TeamMemberNotExists = Error.NotFound
            (code: "teamMember.not.exists", description: "Team Doesn't Have This User");

        public static Error ProjectAlreadyExists = Error.Validation
            (code: "project.already.exists", description: "Project Already Exists in The Team");

        public static Error ProjectNotExists = Error.NotFound
            (code: "project.not.exists", description: "Team Doesn't Have This Project");
    }
}
