using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Models.ResponseModels
{
    public class GetAuditoriumsAndSchedulesResponse
    {
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}