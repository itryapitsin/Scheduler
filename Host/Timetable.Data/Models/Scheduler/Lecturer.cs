using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class Lecturer: BaseIIASEntity
    {
	    public string Lastname { get; set; }
	    public string Firstname { get; set; }
	    public string Middlename { get; set; }
	    public string Contacts { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public Lecturer()
        {
            ScheduleInfoes = new Collection<ScheduleInfo>();
            Departments = new Collection<Department>();
            Positions = new Collection<Position>();
        }
    }
}
