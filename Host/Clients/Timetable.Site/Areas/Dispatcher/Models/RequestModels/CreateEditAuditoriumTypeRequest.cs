using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditAuditoriumTypeRequest
    {
        public string Name { get; set; }

        public string Pattern { get; set; }

        public int? AuditoriumTypeId { get; set; }
    }
}