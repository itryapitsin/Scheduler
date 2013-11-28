using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Models.ViewModels
{
    public class ThreadScheduleViewModel
    {
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<FacultyViewModel> Faculties { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}