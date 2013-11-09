using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class PositionDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        public PositionDataTransfer()
        {
        }

        public PositionDataTransfer(Position position)
        {
            Id = position.Id;
            Name = position.Name;
        }
    }
}
