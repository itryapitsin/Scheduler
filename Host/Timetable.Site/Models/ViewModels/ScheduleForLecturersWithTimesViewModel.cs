using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.ViewModels
{
   
    public class ScheduleForLecturersWithTimesViewModel
    {
        public IList<ScheduleViewModel> Schedules { get; set; }
        public IList<TimeViewModel> Times { get; set; }
    }
}