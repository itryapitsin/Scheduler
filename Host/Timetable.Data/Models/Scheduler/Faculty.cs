using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class Faculty : BaseIIASEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Tutorial> Tutorials { get; set; }
        public virtual Branch Branch { get; set; }
        public int BranchId { get; set; }

        public Faculty()
        {
            Departments = new List<Department>();
            Tutorials = new List<Tutorial>();
            Specialities = new List<Speciality>();
            ScheduleInfoes = new List<ScheduleInfo>();
        }
    }
}
