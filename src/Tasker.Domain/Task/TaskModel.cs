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

        //ctor
        public TaskModel(
            string _name,
            string _status,
            string _priority,
            DateTime _deadline)
        {
            Id = Guid.NewGuid();
            Name = _name;
            Status = _status;
            Priority = _priority;
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
    }
}
