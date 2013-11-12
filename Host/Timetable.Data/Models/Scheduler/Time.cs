using System;
using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
    public class Time : BaseEntity
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int Position { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public Time()
        {
            Buildings = new HashSet<Building>();
        }
    }
}
