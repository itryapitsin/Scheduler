namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class GetFreeAuditoiumsRequest
    {
        public int BuildingId { get; set; }
        public int DayOfWeek { get; set; }
        public int? WeekTypeId { get; set; }
        public int Pair { get; set; }
        public string SubGroup { get; set; }
        public int ScheduleInfoId { get; set; }
        public int? ScheduleId { get; set; }
    }
}