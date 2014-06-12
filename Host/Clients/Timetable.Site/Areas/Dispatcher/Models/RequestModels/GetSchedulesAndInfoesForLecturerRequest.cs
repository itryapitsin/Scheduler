namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class GetSchedulesAndInfoesForLecturerRequest : GetScheduleBaseRequest
    {
        public int LecturerId { get; set; }
    }
}