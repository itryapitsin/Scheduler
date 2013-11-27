using System.Collections.Generic;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class SchedulerViewModel: PageViewModel
    {
        public IEnumerable<int> Pairs { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<WeekTypeViewModel> WeekTypes { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<FacultyViewModel> Faculties { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<ScheduleTypeViewModel> ScheduleTypes { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
        public IEnumerable<ScheduleInfoViewModel> ScheduleInfoes { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public int? CurrentBuildingId { get; set; }
        public int? CurrentStudyYearId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public int? CurrentBranchId { get; set; }
        public int? CurrentFacultyId { get; set; }
        public int? CurrentCourseId { get; set; }
        public int[] CurrentGroupIds { get; set; }
    }
}