using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class TimetableEntityDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }

        public TimetableEntityDataTransfer() { }

        public TimetableEntityDataTransfer(ScheduleType scheduleType)
        {
            Id = scheduleType.Id;
            Name = scheduleType.Name;
            IsActive = scheduleType.IsActive;
        }
    }
}
