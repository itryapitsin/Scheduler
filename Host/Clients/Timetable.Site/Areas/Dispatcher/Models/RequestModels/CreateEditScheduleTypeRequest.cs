using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditScheduleTypeRequest
    {
        public string Name { get; set; }

        public int? ScheduleTypeId { get; set; }
    }
}