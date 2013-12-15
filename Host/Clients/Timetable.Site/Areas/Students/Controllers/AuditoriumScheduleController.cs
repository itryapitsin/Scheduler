using System;
using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Students.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ResponseModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class AuditoriumScheduleController : BaseController
    {
        public ActionResult Index()
        {
            var currentBuildingId = Request.Cookies["currentBuildingId"];
            var currentAuditoriumId = Request.Cookies["currentAuditoriumId"];

            var model = new AuditoriumScheduleViewModel
            {
                Buildings = DataService
                    .GetBuildings()
                    .Select(x => new BuildingViewModel(x)),
            };

            if (currentBuildingId != null)
            {
                model.CurrentBuildingId = Convert.ToInt32(currentBuildingId.Value);
                model.Auditoriums = DataService
                    .GetAuditoriums(model.CurrentBuildingId)
                    .Select(x => new AuditoriumViewModel(x));

                model.Times = DataService
                    .GetTimes(model.CurrentBuildingId)
                    .Select(x => new TimeViewModel(x));
            }

            if (currentAuditoriumId != null)
            {
                var studyYear = DataService.GetStudyYear(DateTime.Now);
                //var semestr = DataService.GetSemesters()

                model.CurrentAuditoriumId = Convert.ToInt32(currentAuditoriumId.Value);
                //TODO: Сделать определение семетра по текущему времени
                model.Schedules = DataService
                    .GetSchedules(new[] {model.CurrentAuditoriumId}, studyYear.Id, 1)
                    .Select(x => new ScheduleViewModel(x));
            }

            return PartialView("_Index", model);
        }

        public ActionResult GetAuditoriums(int buildingId)
        {
            var response = new GetAuditoriumsResponse
            {

                Auditoriums = DataService
                    .GetAuditoriums(buildingId, isTraining: true)
                    .Select(x => new AuditoriumViewModel(x)),
                Times = DataService
                    .GetTimes(buildingId)
                    .Select(x => new TimeViewModel(x))
            };

            return new JsonNetResult(response);
        }

        public ActionResult GetSchedules(int auditoriumId)
        {
            var response = DataService
                .GetSchedulesForAuditorium(
                    auditoriumId,
                    DateTime.Now)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(response);
        }
    }
}
