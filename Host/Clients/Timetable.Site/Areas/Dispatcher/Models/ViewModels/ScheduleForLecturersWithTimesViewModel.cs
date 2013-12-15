using System.Collections.Generic;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class ScheduleForLecturersWithTimesViewModel
    {
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}