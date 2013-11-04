using System;
using System.Runtime.Serialization;

namespace Timetable.Base.Entities.Scheduler
{
    [DataContract(IsReference = true)]
    public class Time : BaseEntity
    {
        [DataMember(Name = "Start")]
        public TimeSpan Start { get; set; }

        [DataMember(Name = "End")]
        public TimeSpan End { get; set; }

        [DataMember(Name = "Position")]
        public int Position { get; set; }
    }
}
