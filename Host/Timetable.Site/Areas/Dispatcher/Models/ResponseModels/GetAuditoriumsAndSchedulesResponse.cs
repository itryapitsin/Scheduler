using System.Collections.Generic;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class GetAuditoriumsAndSchedulesResponse
    {
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}