using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.TeamMember
{
    public class TeamMemberModel : BaseModel
    {
        public Guid UserId { get; private set; }

        public Guid TeamId { get; private set; }

        public bool IsTeamLead { get; private set; }

        //ctor
        public TeamMemberModel(Guid _userId , Guid _teamId, bool _isTeamLead)
        {
            Id = Guid.NewGuid();
            UserId = _userId;
            TeamId = _teamId;
            IsTeamLead = _isTeamLead;
        }

        private TeamMemberModel() { }
    }
}
