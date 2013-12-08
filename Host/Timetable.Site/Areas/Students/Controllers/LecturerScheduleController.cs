using System;
using System.Linq;
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
                var lecturer = DataService.GetLecturerById(Convert.ToInt32(currentLecturerId.Value));
                var studyYear = DataService.GetStudyYear(DateTime.Now);
                model.Lecturer = new LecturerViewModel(lecturer);

                //TODO: Сделать определение семетра по текущему времени
                model.Schedules = DataService
                    .GetSchedulesForLecturer(lecturer.Id, studyYear.Id, 1)
                    .Select(x => new ScheduleViewModel(x));
            }

            return View("_Index", model);
        }

        public ActionResult LoadLecturerSchedule(int lecturerId)
        {
            var studyYear = DataService.GetStudyYear(DateTime.Now);
            //TODO: Сделать определение семетра по текущему времени
            var schedules = DataService
                .GetSchedulesForLecturer(lecturerId, studyYear.Id, 1)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(schedules);
        }

    }
}
