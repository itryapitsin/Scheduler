using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
    public class Speciality: BaseIIASEntity
    {
        public string Name { get; set; }
	    public string ShortName { get; set; }
	    public string Code { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public ICollection<Tutorial> Tutorials { get; set; }
        public ICollection<Group> Groups { get; set; }
        public virtual Branch Branch { get; set; }
        public int BranchId { get; set; }

        public Speciality()
        {
            Faculties = new HashSet<Faculty>();
            ScheduleInfoes = new HashSet<ScheduleInfo>();
        }
    }
}
