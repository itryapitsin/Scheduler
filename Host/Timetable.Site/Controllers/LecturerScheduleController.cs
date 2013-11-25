﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers
{
    public class LecturerScheduleController : AuthorizedController
    {
        public ActionResult Index()
        {
            return View("_Index");
        }

        public ActionResult SearchLecturer(string query)
        {
            var lecturers = DataService
                .GetLecturersByFirstMiddleLastname(query)
                .ToEnumerableWithTotal(x => (new LecturerViewModel(x).Name));

            return new JsonNetResult(lecturers);
        }
    }
}