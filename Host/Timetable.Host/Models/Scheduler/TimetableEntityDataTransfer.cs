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

        public TimetableEntityDataTransfer(TimetableEntity timetableEntity)
        {
            Id = timetableEntity.Id;
            Name = timetableEntity.Name;
            IsActive = timetableEntity.IsActive;
        }
    }
}
