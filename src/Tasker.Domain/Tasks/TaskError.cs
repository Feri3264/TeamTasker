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

        public static Error DeadlineNotValid = Error.Validation
            (code: "deadline.not.valid", description: "Entered Deadline is Not Valid");
    }
}
