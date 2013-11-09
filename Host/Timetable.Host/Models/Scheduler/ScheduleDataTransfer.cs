using System;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class ScheduleDataTransfer : BaseDataTransfer
    {
        public int AuditoriumId { get; set; }
        public int DayOfWeek { get; set; }
        public int PeriodId { get; set; }
        public int ScheduleInfoId { get; set; }
        public int WeekTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AutoDelete { get; set; }
        public int Timetable { get; set; }
        public string SubGroup { get; set; }

        public ScheduleDataTransfer() { }

        public ScheduleDataTransfer(Schedule schedule)
        {
            Id = schedule.Id;

            if (schedule.Auditorium != null)
                AuditoriumId = schedule.Auditorium.Id;

            DayOfWeek = schedule.DayOfWeek;

            if(schedule.Period != null)
                PeriodId = schedule.Period.Id;

            ScheduleInfoId = schedule.ScheduleInfo.Id;
            WeekTypeId = schedule.WeekType.Id;
            StartDate = schedule.StartDate;
            EndDate = schedule.EndDate;
            AutoDelete = schedule.AutoDelete;
            Timetable = schedule.Timetable.Id;
            SubGroup = schedule.SubGroup;
        }
    }
}
