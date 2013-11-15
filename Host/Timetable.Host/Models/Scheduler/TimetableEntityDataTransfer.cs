using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class TimetableEntityDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
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
