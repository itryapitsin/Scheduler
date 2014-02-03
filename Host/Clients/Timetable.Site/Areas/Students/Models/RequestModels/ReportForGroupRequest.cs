using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Students.Models.RequestModels
{
    public class ReportForGroupRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public int StudyYearId { get; set; }

        [Required]
        public int Semester { get; set; }
    }
}