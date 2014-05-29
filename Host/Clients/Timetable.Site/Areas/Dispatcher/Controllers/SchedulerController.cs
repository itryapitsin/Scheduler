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

        public void AuditoriumChanged(int auditoriumId)
        {
            if (auditoriumId != 0)
            {
                UserData.CreatorSettings.CurrentPlanningAuditoriumId = auditoriumId;
                UserService.SaveUserState(UserData);
            }
        }

        public ActionResult BuildingChanged(BuildingChangedRequest request)
        {
            if(request.BuildingId != 0)
                UserData.CreatorSettings.CurrentPlanningBuildingId = request.BuildingId;

            if (request.SubGroup != null)
                UserData.CreatorSettings.CurrentPlanningSubGroup = request.SubGroup;

            if (request.WeekTypeId.HasValue)
                UserData.CreatorSettings.CurrentPlanningWeekTypeId = request.WeekTypeId.Value;

            if (request.ScheduleTypeId != 0)
                 UserData.CreatorSettings.CurrentPlanningScheduleTypeId = request.ScheduleTypeId;

            UserService.SaveUserState(UserData);

            var times = DataService
                .GetTimes(request.BuildingId)
                .Select(x => new TimeViewModel(x));

            var auditoriums =
             DataService
                .GetFreeAuditoriums(request.BuildingId, request.DayOfWeek, 
                request.WeekTypeId, request.Pair, request.ScheduleInfoId, request.SubGroup, request.ScheduleId)
                .Select(x => new AuditoriumViewModel(x));

            return new JsonNetResult(new { Auditoriums = auditoriums, Times = times });
        }

        public ActionResult GetFreeAuditoriums(GetFreeAuditoiumsRequest request)
        {
            if (request.BuildingId != 0)
                UserData.CreatorSettings.CurrentPlanningBuildingId = request.BuildingId;

            if (request.SubGroup != null)
                UserData.CreatorSettings.CurrentPlanningSubGroup = request.SubGroup;

            if (request.WeekTypeId.HasValue)
                UserData.CreatorSettings.CurrentPlanningWeekTypeId = request.WeekTypeId.Value;

            if (request.ScheduleTypeId != 0)
                UserData.CreatorSettings.CurrentPlanningScheduleTypeId = request.ScheduleTypeId;

            UserService.SaveUserState(UserData);

            var result = DataService
                .GetFreeAuditoriums(
                    request.BuildingId,
                    request.DayOfWeek,
                    request.WeekTypeId,
                    request.Pair,
                    request.ScheduleInfoId,
                    request.SubGroup,
                    request.ScheduleId)
                .Select(x => new AuditoriumViewModel(x));

            return new JsonNetResult(result);
        }

        public ActionResult GetSchedulesForSchedule(int scheduleId)
        {
            var result = DataService
                .GetSchedulesForSchedule(scheduleId)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(result);
        }

        public ActionResult GetSchedulesForScheduleInfo(int scheduleInfoId)
        {
            var result = DataService
                .GetSchedulesForScheduleInfo(scheduleInfoId)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(result);
        }

        public ActionResult GetSchedulesAndInfoes(GetSchedulesAndInfoesRequest request)
        {
            var groupIds = GetListFromString(request.GroupIds);

            if (request.FacultyId != 0)
                UserData.CreatorSettings.CurrentFacultyId = request.FacultyId;

            if (request.CourseId != 0)
                UserData.CreatorSettings.CurrentCourseId = request.CourseId;

            if (request.SemesterId != 0)
                UserData.CreatorSettings.CurrentSemesterId = request.SemesterId;

            if (request.StudyYearId != 0)
                UserData.CreatorSettings.CurrentStudyYearId = request.StudyYearId;

            if(groupIds != null)
                UserData.CreatorSettings.CurrentGroupIds = groupIds.ToArray();

            if (request.StudyTypeId != 0)
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
            if (request.SubGroup != null)
            {
                UserData.CreatorSettings.CurrentPlanningSubGroup = request.SubGroup;
                UserService.SaveUserState(UserData);
            }

            try
            {
                var result = DataService.Plan(
                    request.AuditoriumId,
                    request.DayOfWeek,
                    request.ScheduleInfoId,
                    request.TimeId,
                    request.WeekTypeId,
                    request.TypeId,
                    request.SubGroup);

                return new JsonNetResult(
                    new SuccessResponse(
                        new ScheduleViewModel(result)));
            }
            catch (ScheduleCollisionException ex)
            {
                return new JsonNetResult(new FailResponse("Возникла коллизия: проверьте правильность параметров"));
            }
            catch (ScheduleNoDataException ex)
            {
                return new JsonNetResult(new FailResponse("Заполнены не все параметры"));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }

        [HttpPost]
        public ActionResult Edit(EditScheduleRequest request)
        {
            if (request.SubGroup != null)
            {
                UserData.CreatorSettings.CurrentPlanningSubGroup = request.SubGroup;
                UserService.SaveUserState(UserData);
            }

            try
            {
                var result = DataService.PlanEdit(
                    request.AuditoriumId,
                    request.DayOfWeek,
                    request.ScheduleId,
                    request.TimeId,
                    request.WeekTypeId,
                    request.TypeId,
                    request.SubGroup);

                return new JsonNetResult(
                    new SuccessResponse(
                    new ScheduleViewModel(result)));
            }
            catch (ScheduleCollisionException ex)
            {
                return new JsonNetResult(new FailResponse("Возникла коллизия: проверьте правильность параметров"));
            }
            catch (ScheduleNoDataException ex)
            {
                return new JsonNetResult(new FailResponse("Заполнены не все параметры"));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }

        [HttpPost]
        public ActionResult Remove(int scheduleId)
        {
            try
            {
                DataService.Unplan(scheduleId);

                return new JsonNetResult(
                    new SuccessResponse("Занятие успешно удалено"));
            }
            catch (Exception ex)
            {
                return new JsonNetResult(new FailResponse("Возникла ошибка сервера"));
            }
        }
    }
}