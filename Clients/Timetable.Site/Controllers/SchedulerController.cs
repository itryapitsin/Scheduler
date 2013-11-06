using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers
{
    public class SchedulerController: BaseController
    {
        public PartialViewResult Index()
        {
            var model = new SchedulerViewModel();

            model.Times = GenerateTimes();
            model.Buildings = DataService
                .GetBuildings()
                .Select(x => new BuildingViewModel(x));

            model.Branches = DataService
                .GetBranches()
                .Select(x => new BranchViewModel(x));

            return PartialView("_Index", model);
        }

        public TimeViewModel[] GenerateTimes()
        {
            var result = new List<TimeViewModel>
            {
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("8:00"), End = TimeSpan.Parse("9:35")},
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("9:45"), End = TimeSpan.Parse("11:20")},
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("11:30"), End = TimeSpan.Parse("13:05")},
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("13:30"), End = TimeSpan.Parse("15:05")},
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("15:15"), End = TimeSpan.Parse("16:50")},
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("17:00"), End = TimeSpan.Parse("18:35")},
                new TimeViewModel {Id = 12, Start = TimeSpan.Parse("18:35"), End = TimeSpan.Parse("20:00")}
            };

            return result.ToArray();
        }
    }
}