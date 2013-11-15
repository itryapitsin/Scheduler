namespace Timetable.Data.Models.Scheduler
{
    public class ScheduleType : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
