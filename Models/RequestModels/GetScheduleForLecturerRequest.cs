namespace Timetable.Site.Models.RequestModels
{
    public class GetScheduleForLecturerRequest: GetScheduleBaseRequest
    {
        public int LecturerId { get; set; }
    }
}