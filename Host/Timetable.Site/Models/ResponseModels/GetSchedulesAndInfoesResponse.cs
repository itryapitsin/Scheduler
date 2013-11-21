using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.ResponseModels
{
    public class GetSchedulesAndInfoesResponse
    {
        public IEnumerable<ScheduleInfoViewModel> ScheduleInfoes { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
    }
}