using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class UserController : AuthorizedController
    {
        public ActionResult SaveState(UserStateChangedRequest request)
        {
            if (request.BuildingId.HasValue)
            {
                UserData.CreatorSettings.CurrentBuildingId = request.BuildingId;
                UserData.CreatorSettings.CurrentSemesterId = request.SemesterId;
                UserData.CreatorSettings.CurrentStudyYearId = request.StudyYearId;

                UserService.SaveUserState(UserData);


                return new JsonNetResult(DataService
                    .GetTimes(request.BuildingId.Value)
                    .Select(x => new TimeViewModel(x)));
            }

            if (request.BranchId.HasValue)
            {
                UserData.CreatorSettings.CurrentBranchId = request.BranchId;
                UserData.CreatorSettings.CurrentFacultyId = request.FacultyId;
                UserData.CreatorSettings.CurrentCourseId = request.CourseId;
                UserData.CreatorSettings.CurrentGroupIds = GetListFromString(request.GroupIds).ToArray();

                UserService.SaveUserState(UserData);

                if (UserData.CreatorSettings.CurrentFacultyId != null
                    && UserData.CreatorSettings.CurrentCourseId != null
                    && UserData.CreatorSettings.CurrentStudyYearId != null
                    && UserData.CreatorSettings.CurrentSemesterId != null)
                    return new JsonNetResult(DataService
                        .GetScheduleInfoes(
                            UserData.CreatorSettings.CurrentFacultyId.Value,
                            UserData.CreatorSettings.CurrentCourseId.Value,
                            UserData.CreatorSettings.CurrentGroupIds,
                            UserData.CreatorSettings.CurrentStudyYearId.Value,
                            UserData.CreatorSettings.CurrentSemesterId.Value)
                        .Select(x => new ScheduleInfoViewModel(x)));
            }
            return new EmptyResult();
        }
    }
}