using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ResponseModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SessionScheduleController : AuthorizedController
    {
        public PartialViewResult Index()
        {
            return PartialView("_Index");
        }
    }
}