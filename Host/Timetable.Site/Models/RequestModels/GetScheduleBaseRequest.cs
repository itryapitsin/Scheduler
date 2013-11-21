namespace Timetable.Site.Models.RequestModels
{
    public class GetScheduleBaseRequest
    {
        public int StudyYearId { get; set; }
        public int SemesterId { get; set; }
    }
}