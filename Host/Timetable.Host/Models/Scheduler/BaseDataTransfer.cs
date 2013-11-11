using System;
using System.Runtime.Serialization;
using Timetable.Data.Context;
using Timetable.Data.Models;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public abstract class BaseDataTransfer
    {
        [DataMember]
        public int Id { get; set; }
    }
}