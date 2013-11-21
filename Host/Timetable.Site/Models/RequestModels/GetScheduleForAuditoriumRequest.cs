namespace Timetable.Site.Models.RequestModels
{
    public class GetScheduleForAuditoriumRequest: GetScheduleBaseRequest
    {
        public int AuditoriumId { get; set; }
    }
}