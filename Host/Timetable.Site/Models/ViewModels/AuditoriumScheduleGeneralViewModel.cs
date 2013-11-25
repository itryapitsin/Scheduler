using System.Collections.Generic;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class AuditoriumScheduleGeneralViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<AuditoriumTypeViewModel> AuditoriumTypes { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<BusyAuditoriumViewModel> BasyAuditoriums { get; set; }
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
        public IEnumerable<WeekTypeViewModel> WeekTypes { get; set; }
        public int? BuildingId { get; set; }
        public int? AuditoriumId { get; set; }
        public int? AuditoriumTypeId { get; set; }
        public int? StudyYearId { get; set; }
        public int? Semester { get; set; }
    }

    public class BusyAuditoriumViewModel
    {
        public int Id { get; set; }
        public int AuditoriumId { get; set; }
        public int TimeId { get; set; }
        public int DayOfWeek { get; set; }
        public int WeekTypeId { get; set; }

        public BusyAuditoriumViewModel(ScheduleDataTransfer schedule)
        {
            Id = schedule.Id;
            AuditoriumId = schedule.Auditorium.Id;
            TimeId = schedule.Time.Id;
            DayOfWeek = schedule.DayOfWeek;
            WeekTypeId = schedule.WeekType.Id;
        }
    }
}