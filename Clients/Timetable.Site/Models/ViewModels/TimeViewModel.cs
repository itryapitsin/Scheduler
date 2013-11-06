using System;
using System.Runtime.Serialization;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class TimeViewModel
    {
        public int Id;

        public TimeSpan Start;

        public TimeSpan End;
    }
}