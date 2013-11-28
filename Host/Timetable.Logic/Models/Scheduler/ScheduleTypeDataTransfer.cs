using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class ScheduleTypeDataTransfer: BaseDataTransfer
    {
        
        public string Name { get; set; }

        public ScheduleTypeDataTransfer(ScheduleType scheduleType)
        {
            Id = scheduleType.Id;
            Name = scheduleType.Name;
        }
    }
}