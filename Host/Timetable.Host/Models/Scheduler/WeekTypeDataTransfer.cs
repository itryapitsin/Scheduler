using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class WeekTypeDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }

        public WeekTypeDataTransfer() { }

        public WeekTypeDataTransfer(WeekType weekType)
        {
            Id = weekType.Id;
            Name = weekType.Name;
        }
    }
}
