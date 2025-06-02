using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.User
{
    public class UserModel : BaseModel
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool isDelete{ get; private set; }


        //navigation
        private readonly List<Guid> _sessionIds = new(); //set
        public IReadOnlyList<Guid> SessionIds => _sessionIds; //get


        private readonly List<Guid> _sessionMemberIds = new(); //set
        public IReadOnlyList<Guid> SessionMemberIds => _sessionMemberIds; //get


        private readonly List<Guid> _teamMemberIds = new(); //set
        public IReadOnlyList<Guid> TeamMemberIds => _teamMemberIds; //get


        private readonly List<Guid> _projectMemberIds = new(); //set
        public IReadOnlyList<Guid> ProjectMemberIds => _projectMemberIds; //get


        private readonly List<Guid> _taskIds = new(); //set
        public IReadOnlyList<Guid> TaskIds => _taskIds; //get


        //ctor
        public UserModel(
            string _name,
            string _email,
            string _password)
        {
            Id = Guid.NewGuid();
            Name = _name;
            Email = _email;
            Password = _password;
        }

        //methods
        public void SetName(string value)
        {
            Name = value;
        }

        public void SetEmail(string value)
        {
            Email = value;
        }

        public void SetPassword(string value)
        {
            Password = value;
        }

        public void Delete()
        {
            isDelete = !isDelete;
        }

        public void AddSession(Guid sessionId)
        {
            _sessionIds.Add(sessionId);
        }

        public void AddSessionMember(Guid sessionMemberId)
        {
            _sessionMemberIds.Add(sessionMemberId);
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