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
        public string SubGroup { get; set; }
        [Required]
        public int ScheduleTypeId { get; set; }
        [Required]
        public int ScheduleInfoId { get; set; }
        [Required]
        public int? ScheduleId { get; set; }
    }
}