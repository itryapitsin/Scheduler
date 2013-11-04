using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Base.Entities.Scheduler
{
    public class Organization: BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
