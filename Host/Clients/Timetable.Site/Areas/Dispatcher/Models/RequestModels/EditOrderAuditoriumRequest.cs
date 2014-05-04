using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class EditOrderAuditoriumRequest
    {
        [Required]
        public int AuditoriumOrderId { get; set; }
        [Required]
        public string LecturerName { get; set; }
        [Required]
        public string TutorialName { get; set; }
        [Required]
        public string ThreadName { get; set; }
        [Required]
        public bool AutoDelete { get; set; }
    }
}