using System;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class TimeDataTransfer : BaseDataTransfer
    {
        
        public TimeSpan Start { get; set; }
        
        public TimeSpan End { get; set; }
        
        public int Position { get; set; }

        public TimeDataTransfer() { }

        public TimeDataTransfer(Time time)
        {
            if (time != null)
            {
                Id = time.Id;
                Start = time.Start;
                End = time.End;
                Position = time.Position;
            }
        }
    }
}
