using System;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class ScheduleDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public AuditoriumDataTransfer Auditorium { get; set; }
        [DataMember]
        public int DayOfWeek { get; set; }
        [DataMember]
        public TimeDataTransfer Time { get; set; }
        [DataMember]
        public ScheduleInfoDataTransfer ScheduleInfo { get; set; }
        [DataMember]
        public WeekTypeDataTransfer WeekType { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public bool AutoDelete { get; set; }
        [DataMember]
        public TimetableEntityDataTransfer Timetable { get; set; }
        [DataMember]
        public string SubGroup { get; set; }

        public ScheduleDataTransfer() { }

        public ScheduleDataTransfer(Schedule schedule)
        {
            Id = schedule.Id;

            if (schedule.Auditorium != null)
                Auditorium = new AuditoriumDataTransfer(schedule.Auditorium);

            DayOfWeek = schedule.DayOfWeek;

            if(schedule.Period != null)
                Time = new TimeDataTransfer(schedule.Period);

            ScheduleInfo = new ScheduleInfoDataTransfer(schedule.ScheduleInfo);
            WeekType = new WeekTypeDataTransfer(schedule.WeekType);
            AutoDelete = schedule.AutoDelete;
            Timetable = new TimetableEntityDataTransfer(schedule.Timetable);
            SubGroup = schedule.SubGroup;
        }
    }
}
