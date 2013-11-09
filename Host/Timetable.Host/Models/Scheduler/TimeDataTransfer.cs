using System;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class TimeDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public TimeSpan Start { get; set; }
        [DataMember]
        public TimeSpan End { get; set; }
        [DataMember]
        public int Position { get; set; }

        public TimeDataTransfer() { }

        public TimeDataTransfer(Time time)
        {
            Id = time.Id;
            Start = time.Start;
            End = time.End;
            Position = time.Position;
        }
    }
}
