using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class OrderAuditoriumRequest
    {
        [Required]
        public string LecturerName { get; set; }
        [Required]
        public string TutorialName { get; set; }
        [Required]
        public string ThreadName { get; set; }
        [Required]
        public int AuditoriumId { get; set; }
        [Required]
        public int TimeId { get; set; }
        [Required]
        public int DayOfWeek { get; set; }
        [Required]
        public bool AutoDelete { get; set; }
    }
}