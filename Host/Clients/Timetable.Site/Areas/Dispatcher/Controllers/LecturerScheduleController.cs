using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.RequestModels;
using Timetable.Site.Models.ResponseModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class LecturerScheduleController : AuthorizedController
    {
        public PartialViewResult Index()
        {
            var model = new LecturerScheduleViewModel
            {
                StudyYears = DataService
                    .GetStudyYears()
                    .Select(x => new StudyYearViewModel(x)),

                Semesters = DataService
                    .GetSemesters()
                    .Select(x => new SemesterViewModel(x)),

                StudyYearId = UserData.LecturerScheduleSettings.StudyYearId,

                Semester = UserData.LecturerScheduleSettings.SemesterId,

                LecturerSearchString = UserData.LecturerScheduleSettings.LecturerId.HasValue 
                    ? new LecturerViewModel(DataService.GetLecturerById(UserData.LecturerScheduleSettings.LecturerId.Value)).Name
                    : null
            };

            return PartialView("_Index", model);
        }

        public ActionResult SearchLecturer(string query)
        {
            var lecturers = DataService
                .GetLecturersByFirstMiddleLastname(query)
                .ToEnumerableWithTotal(x => (new LecturerViewModel(x).Name));

            return new JsonNetResult(lecturers);
        }

        public ActionResult LoadLecturerSchedule(LecturerScheduleRequest request)
        {
            UserData.LecturerScheduleSettings.SemesterId = request.Semester;
            UserData.LecturerScheduleSettings.StudyYearId = request.StudyYearId;
            UserService.SaveUserState(UserData);

          
            UserData.LecturerScheduleSettings.LecturerId = request.LecturerId;
            UserService.SaveUserState(UserData);

            var schedules = DataService
                .GetSchedulesForLecturer(request.LecturerId, request.StudyYearId, request.Semester)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(new ScheduleForLecturersWithTimesViewModel
                {
                    Schedules = schedules,
                    Times = schedules.Select(x => x.Time)
                });
        }
    }
}
