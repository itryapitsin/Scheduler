using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class AuditoriumDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int? Capacity { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public BuildingDataTransfer Building { get; set; }
        [DataMember]
        public AuditoriumTypeDataTransfer AuditoriumType { get; set; }
        public AuditoriumDataTransfer() {}

        public AuditoriumDataTransfer(Auditorium auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            Name = auditorium.Name;
            Capacity = auditorium.Capacity;
            Info = auditorium.Info;
            Building = new BuildingDataTransfer(auditorium.Building);
            AuditoriumType = new AuditoriumTypeDataTransfer(auditorium.AuditoriumType);
        }
    }
}
