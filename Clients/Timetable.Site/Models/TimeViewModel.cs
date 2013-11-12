using System;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class TimeViewModel
    {
        public int Id;

        public TimeSpan Start;

        public TimeSpan End;

        public TimeViewModel(TimeDataTransfer time)
        {
            Id = time.Id;
            Start = time.Start;
            End = time.End;
        }
    }
}