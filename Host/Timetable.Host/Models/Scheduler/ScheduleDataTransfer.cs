using System;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class ScheduleDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public int AuditoriumId { get; set; }
        [DataMember]
        public int DayOfWeek { get; set; }
        [DataMember]
        public int PeriodId { get; set; }
        [DataMember]
        public int ScheduleInfoId { get; set; }
        [DataMember]
        public int WeekTypeId { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public bool AutoDelete { get; set; }
        [DataMember]
        public int Timetable { get; set; }
        [DataMember]
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
