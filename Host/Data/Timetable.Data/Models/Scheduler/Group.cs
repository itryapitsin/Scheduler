using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class Group : BaseIIASEntity
    {
        public string Code { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public int? StudentsCount { get; set; }
        public Group Parent { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public StudyType StudyType { get; set; }
        public int StudyTypeId { get; set; }

        public Group()
        {
            Courses = new Collection<Course>();
            Specialities = new Collection<Speciality>();
            Faculties = new Collection<Faculty>();
            ScheduleInfoes = new Collection<ScheduleInfo>();
        }
    }
}
