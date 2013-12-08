using System.Collections.Generic;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Models.ViewModels
{
    public class LecturerScheduleViewModel
    {
        public LecturerViewModel Lecturer { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
    }
}