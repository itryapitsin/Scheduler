using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    public class WeekTypeDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }

        public WeekTypeDataTransfer() { }

        public WeekTypeDataTransfer(WeekType weekType)
        {
            Id = weekType.Id;
            Name = weekType.Name;
        }
    }
}
