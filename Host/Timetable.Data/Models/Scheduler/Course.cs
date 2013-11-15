using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
	public class Course: BaseIIASEntity
    {
		public string Name { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }

        public Course()
        {
            ScheduleInfoes = new HashSet<ScheduleInfo>();
        }

	}
}



