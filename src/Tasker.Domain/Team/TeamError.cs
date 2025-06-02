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
        public Error TeamNotFound = Error.NotFound
            (code: "team.not.found" , description: "Team Not Found");
    }
}
