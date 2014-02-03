using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class BuildingsInTimeCreateEditResponse
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public int? CurrentBuildingId { get; set; }
    }
}