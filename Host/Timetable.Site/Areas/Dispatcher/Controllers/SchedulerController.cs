using System;
using System.Linq;
using System.Web.Mvc;
using Timetable.Data.Models.Scheduler;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ResponseModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SchedulerController : AuthorizedController
    {
        public PartialViewResult Index()
        {
            var model = new SchedulerViewModel
            {
                UserName = User.Identity.Name,
                Buildings = DataService
                    .GetBuildings()
                    .Select(x => new BuildingViewModel(x)),

                Branches = DataService
                    .GetBranches()
                    .Select(x => new BranchViewModel(x)),

                WeekTypes = DataService
                    .GetWeekTypes()
                    .Select(x => new WeekTypeViewModel(x)),

                Courses = DataService
                    .GetCources()
                    .OrderBy(x => x.Name)
                    .Select(x => new CourseViewModel(x)),

                StudyYears = DataService
                    .GetStudyYears()
                    .Select(x => new StudyYearViewModel(x)),

                ScheduleTypes = DataService
                    .GetScheduleTypes()
                    .Select(x => new ScheduleTypeViewModel(x)),

                Semesters = DataService
                    .GetSemesters()
                    .Select(x => new SemesterViewModel(x))
            };

            model.Pairs = DataService.GetPairs();

            if (UserData.CreatorSettings.CurrentBuildingId.HasValue)
            {
                model.Auditoriums = DataService
                    .GetAuditoriums(UserData.CreatorSettings.CurrentBuildingId.Value)
                    .Select(x => new AuditoriumViewModel(x));

                model.Times = DataService
                    .GetTimes(UserData.CreatorSettings.CurrentBuildingId.Value)
                    .Select(x => new TimeViewModel(x));

                model.CurrentBuildingId = UserData.CreatorSettings.CurrentBuildingId;
                model.CurrentStudyYearId = UserData.CreatorSettings.CurrentStudyYearId;
                model.CurrentSemesterId = UserData.CreatorSettings.CurrentSemesterId;
            }
            if (UserData.CreatorSettings.CurrentBranchId.HasValue)
            {
                model.Faculties = DataService
                    .GetFaculties(UserData.CreatorSettings.CurrentBranchId.Value)
                    .Select(x => new FacultyViewModel(x));

                model.CurrentBranchId = UserData.CreatorSettings.CurrentBranchId;
                model.CurrentFacultyId = UserData.CreatorSettings.CurrentFacultyId;
                model.CurrentCourseId = UserData.CreatorSettings.CurrentCourseId;
                model.CurrentGroupIds = UserData.CreatorSettings.CurrentGroupIds;

                if (UserData.CreatorSettings.CurrentFacultyId.HasValue
                    && UserData.CreatorSettings.CurrentCourseId.HasValue)
                {
                    model.Groups = DataService
                        .GetGroupsForFaculty(
                            UserData.CreatorSettings.CurrentFacultyId.Value,
                            UserData.CreatorSettings.CurrentCourseId.Value)
                        .Select(x => new GroupViewModel(x));
                }

                if (UserData.CreatorSettings.CurrentFacultyId != null
                    && UserData.CreatorSettings.CurrentCourseId != null
                    && UserData.CreatorSettings.CurrentStudyYearId != null
                    && UserData.CreatorSettings.CurrentSemesterId != null)
                {
                    model.ScheduleInfoes = DataService
                        .GetScheduleInfoes(
                            UserData.CreatorSettings.CurrentFacultyId.Value,
                            UserData.CreatorSettings.CurrentCourseId.Value,
                            UserData.CreatorSettings.CurrentGroupIds,
                            UserData.CreatorSettings.CurrentStudyYearId.Value,
                            UserData.CreatorSettings.CurrentSemesterId.Value)
                        .Select(x => new ScheduleInfoViewModel(x));

                    model.Schedules = DataService
                        .GetSchedulesForFaculty(
                            UserData.CreatorSettings.CurrentFacultyId.Value,
                            UserData.CreatorSettings.CurrentCourseId.Value,
                            UserData.CreatorSettings.CurrentStudyYearId.Value,
                            UserData.CreatorSettings.CurrentSemesterId.Value)
                        .Select(x => new ScheduleViewModel(x));
                }
            }

            return PartialView("_Index", model);
        }

        //public ActionResult BuildingChanged(int buildingId)
        //{
        //    UserData.CreatorSettings.CurrentBuildingId = buildingId;
        //    UserService.SaveUserState(UserData);

        //    var times = DataService
        //        .GetTimes(buildingId)
        //        .Select(x => new TimeViewModel(x));

        //    var auditoriums = DataService
        //        .GetAuditoriums(buildingId)
        //        .Select(x => new AuditoriumViewModel(x));

        //    var response = new GetAuditoriumsResponse
        //    {
        //        Auditoriums = auditoriums,
        //        Times = times
        //    };

        //    return new JsonNetResult(response);
        //}

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
            var schedule = new Schedule
            {
                AuditoriumId = request.AuditoriumId,
                DayOfWeek = request.DayOfWeek,
                ScheduleInfoId = request.ScheduleInfoId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TimeId = request.TimeId,
                WeekTypeId = request.WeekTypeId,
                TypeId = request.TypeId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            DataService.Add(schedule);

            var result = DataService.GetScheduleById(schedule.Id);

            return new JsonNetResult(new ScheduleViewModel(result));
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