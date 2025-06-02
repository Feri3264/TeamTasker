using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Task
{
    public class TaskError
    {
        public Error TaskNotFound = Error.NotFound
            (code: "task.not.found" , description: "Task Not Found");
    }
}
