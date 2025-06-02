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
        public Error ProjectNotFound = Error.NotFound
            (code: "project.not.found" , description: "Project Not Found");
    }
}
