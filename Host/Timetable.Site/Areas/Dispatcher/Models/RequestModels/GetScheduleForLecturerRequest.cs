namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class GetScheduleForLecturerRequest: GetScheduleBaseRequest
    {
        public int LecturerId { get; set; }
    }
}