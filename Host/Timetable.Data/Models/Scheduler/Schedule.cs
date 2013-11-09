using System;

namespace Timetable.Data.Models.Scheduler
{
    public class Schedule : BaseEntity
    {
        public virtual Auditorium Auditorium { get; set; }
        public int? AuditoriumId { get; set; }
        public int DayOfWeek { get; set; }
        public virtual Time Period { get; set; }
        public int PeriodId { get; set; }
        public virtual ScheduleInfo ScheduleInfo { get; set; }
        public int ScheduleInfoId { get; set; }
        public virtual WeekType WeekType { get; set; }
        public int WeekTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AutoDelete { get; set; }
        public virtual TimetableEntity Timetable { get; set; }
        public int? TimetableEntityId { get; set; }
        public string SubGroup { get; set; }
    }
}
