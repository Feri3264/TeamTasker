using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Session
{
    public class SessionModel : BaseModel
    {
        public string Name { get; set; }

        //navigation

        //ctor
        public SessionModel(string _name)
        {
            Name = _name;
        }

        //methods
        public void SetName(string value)
        {
            Name = value;
        }
    }
}
