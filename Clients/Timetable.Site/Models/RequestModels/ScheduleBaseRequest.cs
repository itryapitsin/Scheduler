using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.RequestModels
{
    public class ScheduleBaseRequest
    {
        public int StudyYearId { get; set; }
        public int SemesterId { get; set; }
        public int TimetableId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}