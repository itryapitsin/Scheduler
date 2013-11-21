using Timetable.Data.Models.Personalization;

namespace Timetable.Logic.Models
{
    public class AuditoriumScheduleSettings
    {
        public int? AuditoriumId { get; set; }
        public int? StudyYearId { get; set; }
        public int? SemesterId { get; set; }
        public int? BuildingId { get; set; }

        public AuditoriumScheduleSettings(User user)
        {
            AuditoriumId = user.AuditoriumScheduleSelectedAuditoriumId;
            StudyYearId = user.AuditoriumScheduleSelectedStudyYearId;
            SemesterId = user.AuditoriumScheduleSelectedSemesterId;
            BuildingId = user.AuditoriumScheduleSelectedBuildingId;
        }
    }
}
