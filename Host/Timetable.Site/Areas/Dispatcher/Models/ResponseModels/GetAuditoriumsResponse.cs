using System.Collections.Generic;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class GetAuditoriumsResponse
    {
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}