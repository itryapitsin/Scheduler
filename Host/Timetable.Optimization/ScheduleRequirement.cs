using Timetable.Data.Models.Scheduler;

namespace Timetable.Optimization
{
    public class ScheduleRequirement
    {
        public ScheduleInfo Info { get; set; }
        public double Cost { get; set; }
    }
}
