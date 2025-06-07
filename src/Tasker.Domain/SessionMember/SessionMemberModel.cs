using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.SessionMember
{
    public class SessionMemberModel : BaseModel
    {
        public Guid UserId { get; private set; }

        public Guid SessionId { get; private set; }

        public bool IsOwner { get; private set; }

        //ctor
        public SessionMemberModel(Guid _userid , Guid _sessionid, bool _isOwner)
        {
            Id = Guid.NewGuid();
            UserId = _userid;
            SessionId = _sessionid;
            IsOwner = _isOwner;
        }

        private SessionMemberModel() { }
    }
}
