using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class LecturerScheduleViewModel
    {
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
        public int? StudyYearId { get; set; }
        public int? Semester { get; set; }
        public string LecturerSearchString { get; set; }
    }
}