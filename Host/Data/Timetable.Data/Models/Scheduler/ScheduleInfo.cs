using System;
using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
    public class ScheduleInfo : BaseIIASEntity
    {
        public ICollection<Faculty> Faculties { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Auditorium> LikeAuditoriums { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public int LecturerId { get; set; }
        public virtual TutorialType TutorialType { get; set; }
        public int TutorialTypeId { get; set; }
        public virtual Department Department { get; set; }
        public int? DepartmentId { get; set; }
        public int SubgroupCount { get; set; }
        public decimal HoursPerWeek { get; set; }
        public virtual Tutorial Tutorial { get; set; }
        public int TutorialId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual StudyYear StudyYear { get; set; }
        public int StudyYearId { get; set; }
        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
        public ScheduleInfo()
        {
            Groups = new HashSet<Group>();
            Faculties = new HashSet<Faculty>();
            Courses = new HashSet<Course>();
            Specialities = new HashSet<Speciality>();
            LikeAuditoriums = new HashSet<Auditorium>();
            Schedules = new HashSet<Schedule>();
        }
    }
}
