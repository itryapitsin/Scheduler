using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class TimetableEntityDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public TimetableEntityDataTransfer() { }

        public TimetableEntityDataTransfer(TimetableEntity timetableEntity)
        {
            Id = timetableEntity.Id;
            Name = timetableEntity.Name;
            IsActive = timetableEntity.IsActive;
        }
    }
}
