using System.Collections.Generic;
using Timetable.Site.Models.ResponseModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Models.ViewModels
{
    public class AuditoriumScheduleViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public int CurrentAuditoriumId { get; set; }
        public int CurrentBuildingId { get; set; }

    }
}