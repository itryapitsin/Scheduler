using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
	public class Course: BaseIIASEntity
    {
		public string Name { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public ICollection<Branch> Branches { get; set; }

        public Course()
        {
            Branches = new Collection<Branch>();
            ScheduleInfoes = new HashSet<ScheduleInfo>();
        }

	}
}



