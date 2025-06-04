using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.SessionMember
{
    public class SessionMemberError
    {
        public static Error MembershipNotFound = Error.NotFound
            (code: "member.not.found" , description: "There is No Such a Member in The Session");
    }
}
