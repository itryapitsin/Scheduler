using System.Collections.Generic;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class GetSchedulesAndInfoesResponse
    {
        public IEnumerable<ScheduleInfoViewModel> ScheduleInfoes { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
    }
}