namespace Timetable.Site.Models.RequestModels
{
    public class ScheduleForLecturerRequest: ScheduleBaseRequest
    {
        public int LecturerId { get; set; }
    }
}