using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Tasker.Domain.Common;
using Tasker.Domain.Team;

namespace Tasker.Domain.Session
{
    public class SessionModel : BaseModel
    {
        public string Name { get; private set; }


        //navigation
        public Guid OwnerId { get; private set; }


        private readonly List<Guid> _editors = new(); //set
        public IReadOnlyList<Guid> Editors => _editors; //get
        

        private readonly List<Guid> _teamIds = new(); //set
        public IReadOnlyList<Guid> TeamIds => _teamIds; //get


        //ctor
        private SessionModel(string _name, Guid _ownerId)
        {
            Id = Guid.NewGuid();
            Name = _name;
            OwnerId = _ownerId;
        }

        //methods
        public static ErrorOr<SessionModel> Create(string _name, Guid _ownerId)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return SessionError.NameNotValid;

            return new SessionModel(_name , _ownerId);
        }

        public ErrorOr<Success> SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return SessionError.NameNotValid;

            Name = value;
            return Result.Success;
        }

        public ErrorOr<Success> AddEditor(Guid id)
        {
            if (_editors.Contains(id))
                return SessionError.EditorAlreadyExists;

            _editors.Add(id);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveEditor(Guid id)
        {
            if (_editors.Count == 0 || !_editors.Contains(id))
                return SessionError.EditorNotExists;

            _editors.Remove(id);
            return Result.Success;
        }

        public ErrorOr<Success> AddTeam(Guid teamId)
        {
            if (_teamIds.Contains(teamId))
                return SessionError.TeamAlreadyExists;

            _teamIds.Add(teamId);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveTeam(Guid teamId)
        {
            if (_teamIds.Count == 0 || !_teamIds.Contains(teamId))
                return SessionError.TeamNotExists;

            _teamIds.Remove(teamId);
            return Result.Success;
        }
    }
}
