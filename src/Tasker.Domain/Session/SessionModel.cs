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


        private readonly List<Guid> _sessionMemberIds = new (); //set
        public IReadOnlyList<Guid> SessionMemberIds => _sessionMemberIds; //get


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
        public ErrorOr<SessionModel> Create(string _name, Guid _ownerId)
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

        public ErrorOr<Success> AddSessionMember(Guid sessionMemberId)
        {
            if (_sessionMemberIds.Contains(sessionMemberId))
                return SessionError.SessionMemberAlreadyExists;

            _sessionMemberIds.Add(sessionMemberId);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveSessionMember(Guid sessionMemberId)
        {
            if (_sessionMemberIds.Count == 0 || !_sessionMemberIds.Contains(sessionMemberId))
                return SessionError.SessionMemberNotExists;

            _sessionMemberIds.Remove(sessionMemberId);
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
