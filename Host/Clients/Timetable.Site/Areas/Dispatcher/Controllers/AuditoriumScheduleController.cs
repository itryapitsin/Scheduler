using System;
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
    public class AuditoriumScheduleController : AuthorizedController
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
                var presentationService = new SchedulePresentationFormatService();

                model.Schedules = presentationService.ForAuditoriumFilter(DataService
                .GetSchedulesForAuditorium(
                    UserData.AuditoriumScheduleSettings.AuditoriumId.Value,
                    UserData.AuditoriumScheduleSettings.StudyYearId.Value,
                    UserData.AuditoriumScheduleSettings.SemesterId.Value)
                .ToList()
                .Select(x => new ScheduleViewModel(x)));

            }

            return PartialView("_Index", model);
        }

        public PartialViewResult General()
        {
            var model = new AuditoriumScheduleGeneralViewModel(DataService, UserData);

            return PartialView("_General", model);
        }

        public PartialViewResult Order()
        {
            var model = new AuditoriumScheduleOrderViewModel(DataService, UserData);

            return PartialView("_Order", model);
        }

        /// <summary>
        /// Used in auditoriumScheduleController.js
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Used in auditoriumScheduleGeneralController.js & auditoriumScheduleController.js
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

            var presentationService = new SchedulePresentationFormatService();
            return new JsonNetResult(presentationService.ForAuditoriumFilter(schedules));
        }

        /// <summary>
        /// Used in auditoriumScheduleGeneralController.js
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetAuditoriumsAndSchedules(AuditoriumsScheduleRequest request)
        {
            UserData.AuditoriumScheduleSettings.BuildingId = request.BuildingId;
            UserData.AuditoriumScheduleSettings.AuditoriumId = null;
            UserData.AuditoriumScheduleSettings.SemesterId = request.Semester;
            UserData.AuditoriumScheduleSettings.StudyYearId = request.StudyYearId;
            UserData.AuditoriumScheduleSettings.AuditoriumTypeIds = new List<int> {request.AuditoriumTypeId};
            UserService.SaveUserState(UserData);

            var auditoriums = DataService
                .GetAuditoriums(
                    request.BuildingId,
                    new[] {request.AuditoriumTypeId},
                    true)
                .Select(x => new AuditoriumViewModel(x));

            var schedules = DataService
                .GetSchedules(
                    request.AuditoriumTypeId, 
                    request.StudyYearId, 
                    request.Semester)
                .Select(x => new ScheduleViewModel(x));

            var presentationService = new SchedulePresentationFormatService();

            var times = DataService
                .GetTimes(request.BuildingId)
                .Select(x => new TimeViewModel(x));

            var model = new GetAuditoriumsAndSchedulesResponse
                {
                    Auditoriums = auditoriums,
                    Schedules = presentationService.ForAuditoriumFilter(schedules),
                    Times = times
                };

            return new JsonNetResult(model);
        }

        /// <summary>
        /// Used in auditoriumScheduleOrderController.js
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetAuditoriumsAndOrders(AuditoriumsOrderRequest request)
        {
            UserData.AuditoriumScheduleSettings.BuildingId = request.BuildingId;
            UserData.AuditoriumScheduleSettings.AuditoriumId = null;
            UserData.AuditoriumScheduleSettings.TimeId = request.TimeId;
            UserData.AuditoriumScheduleSettings.DayOfWeek = request.DayOfWeek;
            UserData.AuditoriumScheduleSettings.AuditoriumTypeIds = new List<int> { request.AuditoriumTypeId };
            UserService.SaveUserState(UserData);

            var studyYear = DataService.GetStudyYear(DateTime.Now);
            var semester = DataService.GetSemesterForTime(DateTime.Now);

            var auditoriums = DataService
                .GetAuditoriums(
                    request.BuildingId,
                    new[] { request.AuditoriumTypeId },
                    true)
                .Select(x => new AuditoriumViewModel(x));


            var auditoriumIds = auditoriums.Select(x => x.Id).ToArray();

            var schedules = DataService
                .GetSchedules(
                    auditoriumIds, 
                    studyYear.Id,
                    semester.Id,
                    request.TimeId,
                    request.DayOfWeek)
                .Select(x => new ScheduleViewModel(x));


            var model = new GetAuditoriumsAndOrdersResponse
            {
                Auditoriums = auditoriums,
                AuditoriumOrders = DataService.GetAuditoriumOrders(
                     request.TimeId,
                     request.DayOfWeek,
                     request.BuildingId,
                     new List<int> { request.AuditoriumTypeId }.ToArray()
                     ).Select(x => new AuditoriumOrderViewModel(x)),

                Schedules = schedules
            };

            return new JsonNetResult(model);
        }

        public ActionResult OrderAuditorium(OrderAuditoriumRequest request)
        {
           try
           {
                var result = DataService.PlanAuditoriumOrder(
                    request.TutorialName,
                    request.LecturerName,
                    request.ThreadName,
                    request.DayOfWeek,
                    request.TimeId,
                    request.AuditoriumId,
                    request.AutoDelete);

                return new JsonNetResult(
                    new SuccessResponse(
                        new AuditoriumOrderViewModel(result)));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }

        public ActionResult EditOrderAuditorium(EditOrderAuditoriumRequest request)
        {
            try
            {
                DataService.EditAuditoriumOrder(
                    request.TutorialName,
                    request.LecturerName,
                    request.ThreadName,
                    request.AuditoriumOrderId,
                    request.AutoDelete);

                return new JsonNetResult(
                    new SuccessResponse("Заказ успешно изменен"));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }

        public ActionResult UnOrderAuditorium(int orderId)
        {
            
            try
            {
                DataService.UnplanAuditoriumOrder(orderId);

                return new JsonNetResult(
                    new SuccessResponse("Заказ успешно удален"));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }
    }
}
