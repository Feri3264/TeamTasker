using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Session
{
    public class SessionModel : BaseModel
    {
        public string Name { get; private set; }


        //navigation
        public Guid OwnerId { get; private set; }


        private readonly List<Guid> _sessionMemberIds = new (); //set
        public IReadOnlyList<Guid> SessionMemberIds => _sessionMemberIds; //get


        private readonly List<Guid> _teamIds = new(); //set
        public IReadOnlyList<Guid> TeamIds => _teamIds; //get


        //ctor
        public SessionModel(string _name, Guid ownerId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            OwnerId = ownerId;
        }

        //methods
        public void SetName(string value)
        {
            Name = value;
        }

        public void AddSessionMember(Guid sessionMemberId)
        {
            _sessionMemberIds.Add(sessionMemberId);
        }

        public void AddTeam(Guid teamId)
        {
            _teamIds.Add(teamId);
        }
    }
}
