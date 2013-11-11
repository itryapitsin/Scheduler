using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Timetable.Site.Models;
using System.Web.Security;

namespace Timetable.Site.Controllers
{
    /// <summary>
    /// View controller
    /// </summary>
    public class ScheduleController : BaseController
    {
        [Authorize]
        public ActionResult DatabaseView()
        {
            return RedirectToAction("Index", "Schedule/Database");
        }

        //Обработчик выхода
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Schedule");
        }

        [Authorize]
        public ActionResult InfoForSchedule()
        {
            return View("Config/InfoForSchedule");
        }

        //[Authorize]
        public ActionResult Index()
        {
            return View("NewCreate/Index");
        }

        [Authorize]
        public ActionResult Database()
        {
            return View("Database/Index");
        }

        public ActionResult Group()
        {
            return View("Group/Index");
        }

        [Authorize]
        public ActionResult Auditorium()
        {
            return View("Auditorium/Index");
        }

        [Authorize]
        public ActionResult Lecturer()
        {
            return View("Lecturer/Index");
        }


        [Authorize]
        public ActionResult DatabaseAuditoriums()
        {
            return View("Database/DBAuditoriumIndex");
        }

    }
}
