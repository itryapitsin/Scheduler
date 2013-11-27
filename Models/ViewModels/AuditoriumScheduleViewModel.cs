using System.Collections.Generic;

namespace Timetable.Site.Models.ViewModels
{
    public class AuditoriumScheduleViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
        public int? BuildingId { get; set; }
        public int? AuditoriumId { get; set; }
        public int? StudyYearId { get; set; }
        public int? Semester { get; set; }
    }
}