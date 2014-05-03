using System.Collections.Generic;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Models.ViewModels;
namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class GetAuditoriumsAndOrdersResponse
    {
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<AuditoriumOrderViewModel> AuditoriumOrders { get; set; }

        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
    }
}