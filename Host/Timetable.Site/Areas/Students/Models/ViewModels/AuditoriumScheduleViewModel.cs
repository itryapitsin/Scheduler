using System.Collections.Generic;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using BuildingViewModel = Timetable.Site.Models.ViewModels.BuildingViewModel;
using SemesterViewModel = Timetable.Site.Models.ViewModels.SemesterViewModel;
using StudyYearViewModel = Timetable.Site.Models.ViewModels.StudyYearViewModel;

namespace Timetable.Site.Areas.Students.Models.ViewModels
{
    public class AuditoriumScheduleViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
    }
}