using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class Tutorial : BaseIIASEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public Tutorial()
        {
            Faculties = new Collection<Faculty>();
            Specialities = new Collection<Speciality>();
            ScheduleInfoes = new Collection<ScheduleInfo>();
        }
    }
}
