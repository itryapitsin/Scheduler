using System.Collections.Generic;
using System.Linq;
using Timetable.Data.Models.Personalization;

namespace Timetable.Logic.Models
{
    public class AuditoriumScheduleSettings
    {
        public int? AuditoriumId { get; set; }
        public int? StudyYearId { get; set; }
        public int? SemesterId { get; set; }
        public int? BuildingId { get; set; }
        public IEnumerable<int> AuditoriumTypeIds { get; set; }

        public AuditoriumScheduleSettings(User user)
        {
            AuditoriumId = user.AuditoriumScheduleSelectedAuditoriumId;
            AuditoriumTypeIds = user.AuditoriumScheduleSelectedAuditoriumTypes.Select(x => x.Id);
            StudyYearId = user.AuditoriumScheduleSelectedStudyYearId;
            SemesterId = user.AuditoriumScheduleSelectedSemesterId;
            BuildingId = user.AuditoriumScheduleSelectedBuildingId;
        }
    }
}
