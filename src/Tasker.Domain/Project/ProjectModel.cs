using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Project
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; private set; }


        //navigation
        public Guid TeamId { get; private set; }


        private readonly List<Guid> _taskIds = new(); //set
        public IReadOnlyList<Guid> TaskIds => _taskIds; //get


        private readonly List<Guid> _projectMemberIds = new(); //set
        public IReadOnlyList<Guid> ProjectMwemberIds => _projectMemberIds; //get


        //ctor
        public ProjectModel(string _name, Guid teamId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            TeamId = teamId;
        }

        //methods
        public void SetName(string value)
        {
            Name = value;
        }

        public void AddProjectMember(Guid projectMemberId)
        {
            _projectMemberIds.Add(projectMemberId);
        }

        public void AddTask(Guid taskId)
        {
            _taskIds.Add(taskId);
        }
    }
}
