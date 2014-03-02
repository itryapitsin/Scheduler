using System;
using System.Collections.Generic;
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
                    .GetAuditoriums(model.CurrentBuildingId, isTraining: true)
                    .Select(x => new AuditoriumViewModel(x));

                model.Times = DataService
                    .GetTimes(model.CurrentBuildingId)
                    .Select(x => new TimeViewModel(x));
            }

            if (currentAuditoriumId != null)
            {
                var studyYear = DataService.GetStudyYear(DateTime.Now);
                var semester = DataService.GetSemesterForTime(DateTime.Now);

                model.CurrentAuditoriumId = Convert.ToInt32(currentAuditoriumId.Value);

                var presentationService = new SchedulePresentationFormatService();

                //TODO: Сделать определение семетра по текущему времени
                model.Schedules = presentationService.ForAuditoriumFilter(DataService
                    .GetSchedules(new[] { model.CurrentAuditoriumId }, studyYear.Id, semester.Id)
                    .Select(x => new ScheduleViewModel(x)));
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


            var presentationService = new SchedulePresentationFormatService();

            return new JsonNetResult(presentationService.ForAuditoriumFilter(response));
        }

       
    }

   
}
