using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditTutorialRequest
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public int? TutorialId { get; set; }
    }
}