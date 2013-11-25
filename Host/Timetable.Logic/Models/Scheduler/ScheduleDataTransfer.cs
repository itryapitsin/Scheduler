using System;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class ScheduleDataTransfer : BaseDataTransfer
    {
        
        public AuditoriumDataTransfer Auditorium { get; set; }
        
        public int DayOfWeek { get; set; }
        
        public TimeDataTransfer Time { get; set; }
        
        public ScheduleInfoDataTransfer ScheduleInfo { get; set; }
        
        public WeekTypeDataTransfer WeekType { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public bool AutoDelete { get; set; }
        
        public ScheduleTypeDataTransfer Timetable { get; set; }
        
        public string SubGroup { get; set; }

        public ScheduleDataTransfer() { }

        public ScheduleDataTransfer(Schedule schedule)
        {
            Id = schedule.Id;

            if (schedule.Auditorium != null)
                Auditorium = new AuditoriumDataTransfer(schedule.Auditorium);

            DayOfWeek = schedule.DayOfWeek;

            if(schedule.Time != null)
                Time = new TimeDataTransfer(schedule.Time);

            ScheduleInfo = new ScheduleInfoDataTransfer(schedule.ScheduleInfo);
            WeekType = new WeekTypeDataTransfer(schedule.WeekType);
            AutoDelete = schedule.AutoDelete;
            Timetable = new ScheduleTypeDataTransfer(schedule.Type);
            SubGroup = schedule.SubGroup;
        }
    }
}