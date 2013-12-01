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
    public class LecturerScheduleController : AuthorizedController
    {
        public PartialViewResult Index()
        {
            var sid = UserData.LecturerScheduleSettings.StudyYearId;

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
                LecturerSearchString = UserData.LecturerScheduleSettings.LecturerId.HasValue ?
                                       new LecturerViewModel(DataService.GetLecturerById(UserData.LecturerScheduleSettings.LecturerId.Value)).Name
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

            var currentLecturer = DataService.GetLecturerBySearchQuery(request.LecturerQuery);

            var schedules = new List<ScheduleViewModel>();
            var times = new List<TimeViewModel>();

            if (currentLecturer != null)
            {
                UserData.LecturerScheduleSettings.LecturerId = currentLecturer.Id;
                UserService.SaveUserState(UserData);

                schedules = DataService.GetSchedulesForLecturer(currentLecturer.Id, request.StudyYearId, request.Semester)
                    .ToList()
                    .Select(x => new ScheduleViewModel(x)).ToList();

                var timeIds = schedules.Select(x => x.TimeId).Distinct().ToList();

                times = DataService.GetTimesByIds(timeIds).ToList().Select(x => new TimeViewModel(x)).ToList();
            }

            return new JsonNetResult(new ScheduleForLecturersWithTimesViewModel()
            {
                Schedules = schedules,
                Times = times
            });
        }
    }
}
