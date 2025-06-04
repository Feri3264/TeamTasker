using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ErrorOr;
using Tasker.Domain.Common;
using Tasker.Domain.Project;

namespace Tasker.Domain.Tasks
{
    public class TaskModel : BaseModel
    {
        public string Name { get; private set; }

        public string Status { get; private set; }

        public string Priority { get; private set; }

        public DateTime Deadline { get; private set; }


        //navigation
        public Guid AssignedMemberId { get; private set; }

        public Guid ProjectId { get; private set; }


        //ctor
        private TaskModel(
            string _name,
            string _status,
            string _priority,
            Guid _assignedMemberId,
            Guid _projectId,
            DateTime _deadline)
        {
            Id = Guid.NewGuid();
            Name = _name;
            Status = _status;
            Priority = _priority;
            AssignedMemberId = _assignedMemberId;
            ProjectId = _projectId;
            Deadline = _deadline;
        }

        //methods
        public static ErrorOr<TaskModel> Create(
            string _name,
            string _status,
            string _priority,
            Guid _assignedMemberId,
            Guid _projectId,
            DateTime _deadline)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return ProjectError.NameNotValid;

            if (_deadline < DateTime.UtcNow)
                return TaskError.DeadlineNotValid;

            return new TaskModel(_name , _status , _priority, _assignedMemberId, _projectId, _deadline);
        }
        public ErrorOr<Success> SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ProjectError.NameNotValid;

            Name = value;
            return Result.Success;
        }
        public void SetStatus(string value)
        {
            Status = value;
        }
        public void SetPriority(string value)
        {
            Priority = value;
        }
        public ErrorOr<Success> SetDeadline(DateTime value)
        {
            if (value < DateTime.UtcNow)
                return TaskError.DeadlineNotValid;

            Deadline = value;
            return Result.Success;
        }

        public void ChangeAssignedMember(Guid memberId)
        {
            AssignedMemberId = memberId;
        }
    }
}
