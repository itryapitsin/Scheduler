using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class ReportForLecturerRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int LecturerId { get; set; }
        [Required]
        public int StudyYearId { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}