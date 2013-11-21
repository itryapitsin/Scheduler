using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class ScheduleInfoDataTransfer : BaseDataTransfer
    {
        
        public IEnumerable<FacultyDataTransfer> Faculties { get; set; }
        
        public IEnumerable<CourseDataTransfer> Courses { get; set; }
        
        public IEnumerable<SpecialityDataTransfer> Specialities { get; set; }
        
        public IEnumerable<GroupDataTransfer> Groups { get; set; }
        
        public IEnumerable<AuditoriumDataTransfer> LikeAuditoriums { get; set; }
        
        public LecturerDataTransfer Lecturer { get; set; }
        
        public TutorialTypeDataTransfer TutorialType { get; set; }
        
        public DepartmentDataTransfer Department { get; set; }
        
        public int SubgroupCount { get; set; }
        
        public decimal HoursPerWeek { get; set; }
        
        public TutorialDataTransfer Tutorial { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public StudyYearDataTransfer StudyYear { get; set; }
        
        public int Semester { get; set; }
        public ScheduleInfoDataTransfer()
        {
            Groups = new HashSet<GroupDataTransfer>();
            Faculties = new HashSet<FacultyDataTransfer>();
            Courses = new HashSet<CourseDataTransfer>();
            Specialities = new HashSet<SpecialityDataTransfer>();
            LikeAuditoriums = new HashSet<AuditoriumDataTransfer>();
        }

        public ScheduleInfoDataTransfer(ScheduleInfo scheduleInfo)
        {
            Id = scheduleInfo.Id;
            Groups = scheduleInfo.Groups.Select(x => new GroupDataTransfer(x));
            Faculties = scheduleInfo.Faculties.Select(x => new FacultyDataTransfer(x));
            Courses = scheduleInfo.Courses.Select(x => new CourseDataTransfer(x));
            Specialities = scheduleInfo.Specialities.Select(x => new SpecialityDataTransfer(x));
            LikeAuditoriums = scheduleInfo.LikeAuditoriums.Select(x => new AuditoriumDataTransfer(x));
            Lecturer = new LecturerDataTransfer(scheduleInfo.Lecturer);
            TutorialType = new TutorialTypeDataTransfer(scheduleInfo.TutorialType);
            if (scheduleInfo.Department != null)
                Department = new DepartmentDataTransfer(scheduleInfo.Department);

            SubgroupCount = scheduleInfo.SubgroupCount;
            HoursPerWeek = scheduleInfo.HoursPerWeek;
            Tutorial = new TutorialDataTransfer(scheduleInfo.Tutorial);
            StartDate = scheduleInfo.StartDate;
            EndDate = scheduleInfo.EndDate;

            if (scheduleInfo.StudyYear != null)
                StudyYear = new StudyYearDataTransfer(scheduleInfo.StudyYear);
            Semester = scheduleInfo.SemesterId;
        }
    }
}
