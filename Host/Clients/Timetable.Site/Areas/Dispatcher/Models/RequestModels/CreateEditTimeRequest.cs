using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditTimeRequest
    {
        public string Start { get; set; }

        public string End { get; set; }

        public int Position { get; set; }

        public string BuildingIds { get; set; }

        public int? TimeId { get; set; }
    }
}