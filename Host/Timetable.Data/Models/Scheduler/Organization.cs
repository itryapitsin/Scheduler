using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
    public class Organization: BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
