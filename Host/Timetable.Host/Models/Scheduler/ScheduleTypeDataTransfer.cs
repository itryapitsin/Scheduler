using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class ScheduleTypeDataTransfer: BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }

        public ScheduleTypeDataTransfer(ScheduleType scheduleType)
        {
            Id = scheduleType.Id;
            Name = scheduleType.Name;
        }
    }
}