namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class GetScheduleBaseRequest
    {
        public int StudyTypeId { get; set; }
        public int StudyYearId { get; set; }
        public int SemesterId { get; set; }
    }
}