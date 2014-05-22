using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class BuildingChangedRequest
    {
        [Required]
        public int BuildingId { get; set; }
        [Required]
        public int DayOfWeek { get; set; }
        [Required]
        public int Pair { get; set; }
        [Required]
        public int? WeekTypeId { get; set; }
        [Required]
        public int? ScheduleId { get; set; }
    }
}