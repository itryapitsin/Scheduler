using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class ScheduleInfoDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public IEnumerable<FacultyDataTransfer> Faculties { get; set; }
        [DataMember]
        public IEnumerable<CourseDataTransfer> Courses { get; set; }
        [DataMember]
        public IEnumerable<SpecialityDataTransfer> Specialities { get; set; }
        [DataMember]
        public IEnumerable<GroupDataTransfer> Groups { get; set; }
        [DataMember]
        public IEnumerable<AuditoriumDataTransfer> LikeAuditoriums { get; set; }
        [DataMember]
        public LecturerDataTransfer Lecturer { get; set; }
        [DataMember]
        public TutorialTypeDataTransfer TutorialType { get; set; }
        [DataMember]
        public DepartmentDataTransfer Department { get; set; }
        [DataMember]
        public int SubgroupCount { get; set; }
        [DataMember]
        public decimal HoursPerWeek { get; set; }
        [DataMember]
        public TutorialDataTransfer Tutorial { get; set; }
        [DataMember]
        public DateTime? StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public StudyYearDataTransfer StudyYear { get; set; }
        [DataMember]
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
            Semester = scheduleInfo.Semester;
        }
    }
}
