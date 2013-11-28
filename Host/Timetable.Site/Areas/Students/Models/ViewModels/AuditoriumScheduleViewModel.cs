using System.Collections.Generic;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Models.ViewModels
{
    public class AuditoriumScheduleViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
    }
}