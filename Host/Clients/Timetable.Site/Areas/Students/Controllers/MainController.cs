using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Students/Main/

        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
