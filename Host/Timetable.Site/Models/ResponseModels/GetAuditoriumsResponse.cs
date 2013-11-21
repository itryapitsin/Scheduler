using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.ResponseModels
{
    public class GetAuditoriumsResponse
    {
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }

        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}