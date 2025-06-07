using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ErrorOr;
using Tasker.Domain.Common;
using Tasker.Domain.Team;

namespace Tasker.Domain.Project
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; private set; }

        public Guid LeadId { get; set; }


        //navigation
        public Guid TeamId { get; private set; }


        private readonly List<Guid> _taskIds = new(); //set
        public IReadOnlyList<Guid> TaskIds => _taskIds; //get


        //ctor
        private ProjectModel(string _name, Guid teamId , Guid _leadId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            TeamId = teamId;
            LeadId = _leadId;
        }

        //methods
        public static ErrorOr<ProjectModel> Create(string _name, Guid _teamId , Guid _leadId)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return ProjectError.NameNotValid;

            return new ProjectModel(_name, _teamId , _leadId);
        }

        public ErrorOr<Success> SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ProjectError.NameNotValid;

            Name = value;
            return Result.Success;
        }

        public void ChangeProjectLead(Guid id)
        {
            LeadId = id;
        }

        public ErrorOr<Success> AddTask(Guid taskId)
        {
            if (_taskIds.Contains(taskId))
                return ProjectError.TaskAlreadyExists;

            _taskIds.Add(taskId);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveTask(Guid taskId)
        {
            if (_taskIds.Count == 0 || !_taskIds.Contains(taskId))
                return ProjectError.TaskNotExists;

            _taskIds.Remove(taskId);
            return Result.Success;
        }

        private ProjectModel() { }
    }
}
