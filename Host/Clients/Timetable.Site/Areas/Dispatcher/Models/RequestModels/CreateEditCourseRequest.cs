using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditCourseRequest
    {
      
        public string Name { get; set; }
   
        public string BranchIds { get; set; }

        public int? CourseId { get; set; }
    }
}