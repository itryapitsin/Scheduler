using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditScheduleInfoRequest
    {
                public int SubGroupCount {get; set;}
                public decimal HoursPerWeek { get; set; }
                public string StartDate { get; set; }
                public string EndDate { get; set; }
                public string FacultyIds { get; set; }
                public string CourseIds { get; set; }
                public string GroupIds { get; set; }
                public string LecturerSearchString { get; set; }
                public int SemesterId { get; set; }
                public int DepartmentId { get; set; }
                public int StudyYearId { get; set; }
                public string TutorialSearchString { get; set; }
                public int TutorialTypeId { get; set; }
                public int? ScheduleInfoId { get; set; }
    }
}