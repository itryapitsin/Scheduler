using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditScheduleRequest
    {
        public bool AutoDelete { get; set; }
        public int DayOfWeek { get; set; }
        public string SubGroup { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int AuditoriumId { get; set; }
        public int ScheduleInfoId { get; set; }
        public int TimeId { get; set; }
        public int ScheduleTypeId { get; set; }
        public int WeekTypeId { get; set; }
        public int? ScheduleId { get; set; }
    }
}