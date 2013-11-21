using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;
using Timetable.Site.Models.RequestModels;
using Timetable.Site.Models.ResponseModels;

namespace Timetable.Site.Controllers
{
    public class AuditoriumScheduleController : NewBaseController
    {
        public PartialViewResult Index()
        {
            var model = new AuditoriumScheduleViewModel
            {
                Buildings = DataService
                    .GetBuildings()
                    .Select(x => new BuildingViewModel(x)),

                BuildingId = UserData.AuditoriumScheduleSettings.BuildingId,

                AuditoriumId = UserData.AuditoriumScheduleSettings.AuditoriumId,

                StudyYears = DataService
                    .GetStudyYears()
                    .Select(x => new StudyYearViewModel(x)),

                Semesters = DataService
                    .GetSemesters()
                    .Select(x => new SemesterViewModel(x)),

                StudyYearId = UserData.AuditoriumScheduleSettings.StudyYearId,

                Semester = UserData.AuditoriumScheduleSettings.SemesterId
            };

            if (UserData.AuditoriumScheduleSettings.BuildingId.HasValue)
            {
                model.Auditoriums = DataService
                    .GetAuditoriums(UserData.AuditoriumScheduleSettings.BuildingId.Value)
                    .Select(x => new AuditoriumViewModel(x))
                    .OrderBy(x => x.Number);

                model.Times = DataService
                    .GetTimes(UserData.AuditoriumScheduleSettings.BuildingId.Value)
                    .Select(x => new TimeViewModel(x));
            }

            if (UserData.AuditoriumScheduleSettings.AuditoriumId.HasValue
                && UserData.AuditoriumScheduleSettings.SemesterId.HasValue
                && UserData.AuditoriumScheduleSettings.StudyYearId.HasValue)
            {
                model.Schedules = DataService
                .GetSchedulesForAuditorium(
                    UserData.AuditoriumScheduleSettings.AuditoriumId.Value, 
                    UserData.AuditoriumScheduleSettings.StudyYearId.Value, 
                    UserData.AuditoriumScheduleSettings.SemesterId.Value)
                .ToList()
                .Select(x => new ScheduleViewModel(x));

            }

            return PartialView("_Index", model);
        }

        public ActionResult GetAuditoriums(int buildingId)
        {
            UserData.AuditoriumScheduleSettings.BuildingId = buildingId;
            UserData.AuditoriumScheduleSettings.AuditoriumId = null;
            UserService.SaveUserState(UserData);

            var auditoriums = DataService
                .GetAuditoriums(buildingId)
                .Select(x => new AuditoriumViewModel(x))
                .OrderBy(x => x.Number);

            var times = DataService
                .GetTimes(buildingId)
                .Select(x => new TimeViewModel(x))
                .OrderBy(x => x.Start);

            var model = new GetAuditoriumsResponse
            {
                Auditoriums = auditoriums,
                Times = times
            };

            return new JsonNetResult(model);
        }

        public ActionResult LoadAuditoriumSchedule(AuditoriumScheduleRequest request)
        {
            UserData.AuditoriumScheduleSettings.SemesterId = request.Semester;
            UserData.AuditoriumScheduleSettings.StudyYearId = request.StudyYearId;
            UserData.AuditoriumScheduleSettings.AuditoriumId = request.AuditoriumId;
            UserService.SaveUserState(UserData);

            var schedules = DataService
                .GetSchedulesForAuditorium(request.AuditoriumId, request.StudyYearId, request.Semester)
                .ToList()
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(schedules);
        }
    }
}
