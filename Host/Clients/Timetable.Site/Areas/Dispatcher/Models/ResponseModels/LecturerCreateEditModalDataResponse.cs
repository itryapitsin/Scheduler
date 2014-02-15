using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class LecturerCreateEditModalDataResponse
    {
        public IEnumerable<DepartmentViewModel> Departments { get; set; }
        public IEnumerable<PositionViewModel> Positions { get; set; }
    }
}