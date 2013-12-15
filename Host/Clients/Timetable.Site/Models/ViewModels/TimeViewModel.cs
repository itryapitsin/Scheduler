using System;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class TimeViewModel
    {
        public int Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int Position { get; set; }

        public TimeViewModel(TimeDataTransfer time)
        {
            Id = time.Id;
            Start = time.Start;
            End = time.End;
            Position = time.Position;
        }
    }
}