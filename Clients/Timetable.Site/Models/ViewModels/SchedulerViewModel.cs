using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.ViewModels
{
    public class SchedulerViewModel
    {
        public IEnumerable<TimeViewModel> Times { get; set; }

        public IEnumerable<BuildingViewModel> Buildings { get; set; }

        public IEnumerable<WeekTypeViewModel> WeekTypes { get; set; }

        public IEnumerable<BranchViewModel> Branches { get; set; }

        public BuildingViewModel SelectedBuidling { get; set; }
    }
}