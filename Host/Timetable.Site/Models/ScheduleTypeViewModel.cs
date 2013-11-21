using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class ScheduleTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ScheduleTypeViewModel(ScheduleTypeDataTransfer scheduleType)
        {
            Id = scheduleType.Id;
            Name = scheduleType.Name;
        }
    }
}