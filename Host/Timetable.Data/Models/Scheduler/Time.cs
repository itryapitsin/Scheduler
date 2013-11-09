using System;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class Time : BaseEntity
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int Position { get; set; }
    }
}
