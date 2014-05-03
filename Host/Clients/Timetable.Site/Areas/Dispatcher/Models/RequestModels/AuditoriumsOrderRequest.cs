using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class AuditoriumsOrderRequest
    {
        [Required]
        public int BuildingId { get; set; }
        [Required]
        public int AuditoriumTypeId { get; set; }
        [Required]
        public int TimeId { get; set; }
        [Required]
        public int DayOfWeek { get; set; }
    }
}