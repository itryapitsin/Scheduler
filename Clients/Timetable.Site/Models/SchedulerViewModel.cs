using System.Collections.Generic;

namespace Timetable.Site.Models
{
    public class SchedulerViewModel
    {
        public IEnumerable<TimeViewModel> Times { get; set; }

        public IEnumerable<BuildingViewModel> Buildings { get; set; }

        public IEnumerable<WeekTypeViewModel> WeekTypes { get; set; }

        public IEnumerable<BranchViewModel> Branches { get; set; }

        public BuildingViewModel SelectedBuidling { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }

        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
    }
}