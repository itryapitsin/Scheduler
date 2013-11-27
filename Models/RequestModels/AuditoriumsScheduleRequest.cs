using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Models.RequestModels
{
    public class AuditoriumsScheduleRequest
    {
        public int BuildingId { get; set; }
        [Required]
        public int AuditoriumTypeId { get; set; }
        [Required]
        public int StudyYearId { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}