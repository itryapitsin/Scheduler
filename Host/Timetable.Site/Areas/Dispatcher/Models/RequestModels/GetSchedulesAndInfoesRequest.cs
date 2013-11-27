namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class GetSchedulesAndInfoesRequest: GetScheduleBaseRequest
    {
        public int FacultyId { get; set; }
        public int CourseId { get; set; }
        public string GroupIds { get; set; }
    }
}