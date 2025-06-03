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


        //navigation
        public Guid TeamId { get; private set; }


        private readonly List<Guid> _taskIds = new(); //set
        public IReadOnlyList<Guid> TaskIds => _taskIds; //get


        private readonly List<Guid> _projectMemberIds = new(); //set
        public IReadOnlyList<Guid> ProjectMwemberIds => _projectMemberIds; //get


        //ctor
        private ProjectModel(string _name, Guid teamId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            TeamId = teamId;
        }

        //methods
        public ErrorOr<ProjectModel> Create(string _name, Guid teamId)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return ProjectError.NameNotValid;

            return new ProjectModel(_name, teamId);
        }

        public ErrorOr<Success> SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ProjectError.NameNotValid;

            Name = value;
            return Result.Success;
        }

        public ErrorOr<Success> AddProjectMember(Guid projectMemberId)
        {
            if (_projectMemberIds.Contains(projectMemberId))
                return ProjectError.ProjectMemberAlreadyExists;

            _projectMemberIds.Add(projectMemberId);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveProjectMember(Guid projectMemberId)
        {
            if (_projectMemberIds.Count == 0 || !_projectMemberIds.Contains(projectMemberId))
                return ProjectError.ProjectMemberNotExists;

            _projectMemberIds.Remove(projectMemberId);
            return Result.Success;
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
    }
}
