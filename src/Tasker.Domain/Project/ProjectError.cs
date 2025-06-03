using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Project
{
    public class ProjectError
    {
        public static Error ProjectNotFound = Error.NotFound
            (code: "project.not.found" , description: "Project Not Found");

        public static Error NameNotValid = Error.Validation
            (code: "name.not.valid", description: "Entered Name is Not Valid");

        public static Error ProjectMemberAlreadyExists = Error.Validation
            (code: "projectMember.already.exists", description: "Project Already Has This User");

        public static Error ProjectMemberNotExists = Error.NotFound
            (code: "projectMember.not.exists", description: "Project Doesn't Have This User");

        public static Error TaskAlreadyExists = Error.Validation
            (code: "task.already.exists", description: "Task Already Exists in The Project");

        public static Error TaskNotExists = Error.NotFound
            (code: "task.not.exists", description: "Project Doesn't Have This Task");
    }
}
