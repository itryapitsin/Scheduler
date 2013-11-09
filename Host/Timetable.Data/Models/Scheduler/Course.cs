using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
	public class Course: BaseEntity
    {
		public string Name { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }

        public Course()
        {
            ScheduleInfoes = new HashSet<ScheduleInfo>();
        }

	}
}



