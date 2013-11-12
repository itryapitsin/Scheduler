using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models;
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
        public int BuildingId { get; set; }
        [DataMember]
        public int AuditoriumTypeId { get; set; }
        public AuditoriumDataTransfer() {}

        public AuditoriumDataTransfer(Auditorium auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            Name = auditorium.Name;
            Capacity = auditorium.Capacity;
            Info = auditorium.Info;
            BuildingId = auditorium.Building.Id;
            AuditoriumTypeId = auditorium.AuditoriumType.Id;
        }
    }
}
