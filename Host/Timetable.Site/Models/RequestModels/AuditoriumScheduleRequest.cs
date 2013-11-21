using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Models.RequestModels
{
    public class AuditoriumScheduleRequest
    {
        [Required]
        public int AuditoriumId { get; set; }
        [Required]
        public int StudyYearId { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}