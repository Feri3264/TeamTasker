using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Tasks
{
    public class TaskError
    {
        public static Error TaskNotFound = Error.NotFound
            (code: "task.not.found" , description: "Task Not Found");

        public static Error NameNotValid = Error.Validation
            (code: "name.not.valid", description: "Entered Name is Not Valid");

        public static Error StatusNotValid = Error.Validation
            (code: "status.not.valid", description: "Invalid Status Value");

        public static Error PriorityNotValid = Error.Validation
            (code: "priority.not.valid", description: "Invalid Priority Value");

        public static Error DeadlineNotValid = Error.Validation
            (code: "deadline.not.valid", description: "Entered Deadline is Not Valid");
    }
}
