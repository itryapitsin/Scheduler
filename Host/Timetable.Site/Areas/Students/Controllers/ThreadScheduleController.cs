using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class ThreadScheduleController : Controller
    {
        public ActionResult Index()
        {
            return View("_Index");
        }

    }
}
