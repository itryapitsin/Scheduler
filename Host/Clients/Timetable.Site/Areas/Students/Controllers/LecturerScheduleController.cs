using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Timetable.Site.Areas.Students.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class LecturerScheduleController : BaseController
    {
        public ActionResult Index()
        {

            var currentLecturerId = Request.Cookies["currentLecturerId"];

            var model = new LecturerScheduleViewModel();

            if (currentLecturerId != null)
            {

                int lecturerId = int.Parse(currentLecturerId.Value);
                //Сконвертировать currentLecturerSearchString.Value

                var studyYear = DataService.GetStudyYear(DateTime.Now);
                var semester = DataService.GetSemesterForTime(DateTime.Now);

                model.Lecturer = new LecturerViewModel(DataService.GetLecturerById(lecturerId));

                model.Schedules = DataService
                    .GetSchedulesForLecturer(lecturerId, studyYear.Id, semester.Id)
                    .Select(x => new ScheduleViewModel(x));

            }


            return View("_Index", model);
        }


        public ActionResult LoadLecturers(string searchString)
        {
            var lecturers = searchString.Length > 3 ? DataService.GetLecturersByFirstMiddleLastname(searchString)
                .Select(x => new LecturerViewModel(x))
                : new List<LecturerViewModel>();

            return new JsonNetResult(lecturers);
        }

        //old
        //public ActionResult LoadLecturerSchedule(int lecturerId)
        //new
        public ActionResult LoadLecturerSchedule(int lecturerId)
        {
            var studyYear = DataService.GetStudyYear(DateTime.Now);
            var semester = DataService.GetSemesterForTime(DateTime.Now);

            var schedules = DataService
                .GetSchedulesForLecturer(lecturerId, studyYear.Id, semester.Id)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(schedules);
        }

    }
}
