using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.User
{
    public class UserError
    {
        public Error UserNotFound = Error.NotFound
            (code: "user.not.found" , description: "User Not Found");
    }
}
