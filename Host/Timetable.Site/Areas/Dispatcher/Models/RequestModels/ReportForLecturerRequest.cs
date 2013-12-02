using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class ReportForLecturerRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string LecturerQuery { get; set; }
        [Required]
        public int StudyYearId { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}