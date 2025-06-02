using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Task
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
        public TaskModel(
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
        public void SetName(string value)
        {
            Name = value;
        }
        public void SetStatus(string value)
        {
            Status = value;
        }
        public void SetPriority(string value)
        {
            Priority = value;
        }
        public void SetDeadline(DateTime value)
        {
            Deadline = value;
        }

        public void ChangeAssignedMember(Guid memberId)
        {
            AssignedMemberId = memberId;
        }
    }
}
