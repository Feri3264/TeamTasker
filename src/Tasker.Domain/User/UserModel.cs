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
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool isDelete{ get; set; }

        //navigation


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
    }
}