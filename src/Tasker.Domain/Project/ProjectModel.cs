using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.Project
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; private set; }

        //navigation

        //ctor
        public ProjectModel(string _name)
        {
            Id = Guid.NewGuid();
            Name = _name;
        }

        //methods
        public void SetName(string value)
        {
            Name = value;
        }
    }
}
