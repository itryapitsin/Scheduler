using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditGroupRequest
    {
        public string Code { get; set; }

        public int StudentsCount { get; set; }

        public string FacultyIds { get; set; }

        public string CourseIds { get; set; }

        public int StudyTypeId { get; set; }

        public bool isActual { get; set; }

        public int? GroupId { get; set; }
    }
}