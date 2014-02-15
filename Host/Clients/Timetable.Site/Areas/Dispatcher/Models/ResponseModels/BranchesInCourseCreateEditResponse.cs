using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class BranchesInCourseCreateEditResponse
    {
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public int? CurrentBranchId { get; set; }
    }
}