using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class Organization: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Branch> Branches { get; set; }

        public Organization()
        {
            Branches = new Collection<Branch>();
        }
    }
}
