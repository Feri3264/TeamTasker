using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Team
{
    public class TeamModel : BaseModel
    {
        public string Name { get; private set; }


        //navigation
        public Guid SessionId { get; private set; }


        private readonly List<Guid> _projectIds = new();
        public IReadOnlyList<Guid> ProjectIds => _projectIds;


        private readonly List<Guid> _teamMemberIds = new();
        public IReadOnlyList<Guid> TeamMemberIds => _teamMemberIds;


        //ctor
        public TeamModel(string _name , Guid _sessionId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            SessionId = _sessionId;
        }

        //methods
        public void SetName(string value)
        {
            Name = value;
        }

        public void AddTeamMember(Guid teamMemberId)
        {
            _teamMemberIds.Add(teamMemberId);
        }

        public void AddProject(Guid projectId)
        {
            _projectIds.Add(projectId);
        }
    }
}
