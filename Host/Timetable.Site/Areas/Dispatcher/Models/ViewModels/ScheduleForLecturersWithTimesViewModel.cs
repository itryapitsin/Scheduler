using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class ScheduleForLecturersWithTimesViewModel
    {
        public IList<ScheduleViewModel> Schedules { get; set; }
        public IList<TimeViewModel> Times { get; set; }
    }
}