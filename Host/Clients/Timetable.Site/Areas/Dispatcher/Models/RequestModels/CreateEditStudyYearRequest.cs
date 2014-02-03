using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditStudyYearRequest
    {
        public string StartYear { get; set; }

        public int Length { get; set; }

        public int? StudyYearId { get; set; }
    }
}