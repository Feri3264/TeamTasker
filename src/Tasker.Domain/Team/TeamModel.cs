using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ErrorOr;
using Tasker.Domain.Common;
using Tasker.Domain.Session;

namespace Tasker.Domain.Team
{
    public class TeamModel : BaseModel
    {
        public string Name { get; private set; }

        public Guid LeadId { get; private set; }


        //navigation
        public Guid SessionId { get; private set; }


        private readonly List<Guid> _projectIds = new(); //set
        public IReadOnlyList<Guid> ProjectIds => _projectIds; //get


        //ctor
        private TeamModel(string _name , Guid _sessionId , Guid _leadId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            SessionId = _sessionId;
            LeadId = _leadId;
        }

        //methods
        public static ErrorOr<TeamModel> Create(string _name , Guid _sessionId, Guid _leadId)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return TeamError.NameNotValid;

            return new TeamModel(_name , _sessionId , _leadId);
        }
        public ErrorOr<Success> SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return TeamError.NameNotValid;

            Name = value;
            return Result.Success;
        }

        public void ChangeTeamLead(Guid id)
        {
            LeadId = id;
        }

        public ErrorOr<Success> AddProject(Guid projectId)
        {
            if (_projectIds.Contains(projectId))
                return TeamError.ProjectAlreadyExists;

            _projectIds.Add(projectId);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveProject(Guid projectId)
        {
            if (_projectIds.Count == 0 || !_projectIds.Contains(projectId))
                return TeamError.ProjectNotExists;

            _projectIds.Remove(projectId);
            return Result.Success;
        }

        private TeamModel() { }
    }
}
