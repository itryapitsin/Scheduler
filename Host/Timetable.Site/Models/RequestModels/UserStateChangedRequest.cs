using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.RequestModels
{
    public class UserStateChangedRequest
    {
        public int? BuildingId { get; set; }
        public int? StudyYearId { get; set; }
        public int? SemesterId { get; set; }
        public int? WeekTypeId { get; set; }
        public int? BranchId { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public string GroupIds { get; set; }
    }
}