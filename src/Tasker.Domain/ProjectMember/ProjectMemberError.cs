using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace Tasker.Domain.ProjectMember
{
    public class ProjectMemberError
    {
        public static Error MembershipNotFound = Error.NotFound
            (code: "membership.not.found", description: "There is No Such a Membership");

        public static Error MemberNotFound = Error.NotFound
            (code: "member.not.found", description: "There is No Such a Member in The Team");
    }
}
