using System.Collections.Generic;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Models.ResponseModels
{
    public class GetAuditoriumsResponse
    {
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}