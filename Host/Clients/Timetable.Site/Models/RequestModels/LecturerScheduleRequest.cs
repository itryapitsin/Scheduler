using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Models.RequestModels
{
    public class LecturerScheduleRequest
    {
        [Required]
        public int LecturerId { get; set; }
        [Required]
        public int StudyYearId { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}