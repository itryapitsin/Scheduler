using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditLecturerRequest
    {
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Contacts { get; set; }

        public string PositionIds { get; set; }
    
        public string DepartmentIds { get; set; }

        public int? LecturerId { get; set; }
    }
}