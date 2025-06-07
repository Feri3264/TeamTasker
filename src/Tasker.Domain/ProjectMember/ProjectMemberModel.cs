using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.ProjectMember
{
    public class ProjectMemberModel : BaseModel
    {
        public Guid UserId { get; private set; }

        public Guid ProjectId { get; private set; }

        public bool IsProjectLead { get; set; }

        //ctor
        public ProjectMemberModel(Guid _userId , Guid _projectId, bool _isProjectLead)
        {
            Id = Guid.NewGuid();
            UserId = _userId;
            ProjectId = _projectId;
            IsProjectLead = _isProjectLead;
        }

        private ProjectMemberModel() { }
    }
}
