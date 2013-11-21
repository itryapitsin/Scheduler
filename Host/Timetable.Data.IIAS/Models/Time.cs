using System;

namespace Timetable.Data.IIAS.Models
{
    public class Time
    {
        public Int64 Id { get; set; }

        public string Start { get; set; }

        public string Finish { get; set; }
        public Int64 Position { get; set; }
    }
}
