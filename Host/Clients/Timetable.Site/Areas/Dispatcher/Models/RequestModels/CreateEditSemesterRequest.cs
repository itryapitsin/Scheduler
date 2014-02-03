using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditSemesterRequest
    {
        public string Name { get; set; }

        public int? SemesterId { get; set; }
    }
}