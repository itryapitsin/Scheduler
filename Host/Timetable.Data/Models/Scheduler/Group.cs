using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;

namespace Timetable.Data.Models.Scheduler
{
    public class Group : BaseEntity
    {
        public string Code { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public virtual Speciality Speciality { get; set; }
        public int SpecialityId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public int FacultyId { get; set; }

        public int? StudentsCount { get; set; }
        public Group Parent { get; set; }

        //[DataMember(Name = "ParentId")]
        //public int? ParentId { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }

        public Group()
        {
            ScheduleInfoes = new Collection<ScheduleInfo>();
        }
    }
}
