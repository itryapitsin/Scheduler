using System;
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
            //var currentLecturerId = Request.Cookies["currentLecturerId"];
            var currentLecturerSearchString = Request.Cookies["currentLecturerSearchString"];

            var model = new LecturerScheduleViewModel();

            if (currentLecturerSearchString != null)
            {
                var lecturer = DataService.GetLecturerBySearchString(currentLecturerSearchString.Value);

                //Сконвертировать currentLecturerSearchString.Value


                if (lecturer != null)
                {
                    var studyYear = DataService.GetStudyYear(DateTime.Now);
                    var semester = DataService.GetSemesterForTime(DateTime.Now);

                    model.Lecturer = new LecturerViewModel(lecturer);

                    model.Schedules = DataService
                        .GetSchedulesForLecturer(lecturer.Id, studyYear.Id, semester.Id)
                        .Select(x => new ScheduleViewModel(x));
                }
            }


            return View("_Index", model);
        }


        //old
        //public ActionResult LoadLecturerSchedule(int lecturerId)
        //new
        public ActionResult LoadLecturerSchedule(string searchString)
        {
            var lecturerId = 0;
            var lecturer = DataService.GetLecturerBySearchString(searchString);
            if (lecturer != null)
                lecturerId = lecturer.Id;

            var studyYear = DataService.GetStudyYear(DateTime.Now);
            var semester = DataService.GetSemesterForTime(DateTime.Now);

            var schedules = DataService
                .GetSchedulesForLecturer(lecturerId, studyYear.Id, semester.Id)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(schedules);
        }

    }
}
