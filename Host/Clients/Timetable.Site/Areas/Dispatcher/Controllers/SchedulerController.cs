using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Exceptions;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SchedulerController : AuthorizedController
    {
        public PartialViewResult Index()
        {
            var model = new SchedulerViewModel(DataService, UserData);

            return PartialView("_Index", model);
        }

        public ActionResult BranchChanged(int branchId)
        {
            var faculties = DataService
                .GetFaculties(branchId)
                .Select(x => new FacultyViewModel(x));

            var courses = DataService
                .GetCources(branchId)
                .Select(x => new CourseViewModel(x));

            return new JsonNetResult(new { Faculties = faculties, Courses = courses });
        }

        public ActionResult BuildingChanged(int buildingId)
        {
            var times = DataService
                .GetTimes(buildingId)
                .Select(x => new TimeViewModel(x));

            var auditoriums = DataService
                .GetAuditoriums(buildingId, isTraining: true)
                .Select(x => new AuditoriumViewModel(x));

            return new JsonNetResult(new { Auditoriums = auditoriums, Times = times });
        }

        public ActionResult GetFreeAuditoriums(GetFreeAuditoiumsRequest request)
        {
            var result = DataService
                .GetFreeAuditoriums(
                    request.BuildingId,
                    request.DayOfWeek,
                    request.WeekTypeId,
                    request.Pair)
                .Select(x => new AuditoriumViewModel(x));

            return new JsonNetResult(result);
        }

        public ActionResult GetSchedulesAndInfoes(GetSchedulesAndInfoesRequest request)
        {
            var groupIds = GetListFromString(request.GroupIds);

            UserData.CreatorSettings.CurrentFacultyId = request.FacultyId;
            UserData.CreatorSettings.CurrentCourseId = request.CourseId;
            UserData.CreatorSettings.CurrentSemesterId = request.SemesterId;
            UserData.CreatorSettings.CurrentStudyYearId = request.StudyYearId;
            UserData.CreatorSettings.CurrentGroupIds = groupIds.ToArray();
            UserData.CreatorSettings.CurrentStudyTypeId = request.StudyTypeId;
            UserService.SaveUserState(UserData);
            
            var response = new GetSchedulesAndInfoesResponse
            {
                ScheduleInfoes = DataService
                    .GetScheduleInfoes(
                        request.FacultyId,
                        request.CourseId,
                        groupIds.ToArray(),
                        request.StudyYearId,
                        request.SemesterId)
                    .Select(x => new ScheduleInfoViewModel(x)),

                Schedules = DataService
                    .GetSchedules(
                        request.FacultyId,
                        request.CourseId,
                        groupIds.ToArray(),
                        request.StudyYearId,
                        request.SemesterId)
                    .Select(x => new ScheduleViewModel(x))
            };

            return new JsonNetResult(response);
        }

        [HttpPost]
        public ActionResult Create(CreateScheduleRequest request)
        {
            try
            {
                var result = DataService.Plan(
                    request.AuditoriumId,
                    request.DayOfWeek,
                    request.ScheduleInfoId,
                    request.TimeId,
                    request.WeekTypeId,
                    request.TypeId);

                return new JsonNetResult(
                    new SuccessResponse(
                        new ScheduleViewModel(result)));
            }
            catch (ScheduleCollisionException ex)
            {
                return new JsonNetResult(new FailResponse("Возникла коллизия: проверте правильность параметров"));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }

        [HttpPost]
        public ActionResult Edit(EditScheduleRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}