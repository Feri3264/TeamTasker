using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace Tasker.Domain.TeamMember
{
    public class TeamMemberError
    {
        public Error MemberNotFound = Error.NotFound
            (code: "member.not.found" , description: "Member with this Id Not Found in The Team");
    }
}
