using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using log4net.Util;
using Timetable.Site.Areas.Students.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class ThreadScheduleController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ThreadScheduleViewModel(Request, DataService);
            return View("_Index", model);
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

        public ActionResult GetGroups(int facultyId, int courseId, int studyTypeId)
        {
            var response = DataService
                .GetGroupsForFaculty(facultyId, courseId, studyTypeId)
                .OrderBy(x => x.Code)
                .Select(x => new GroupViewModel(x));

            return new JsonNetResult(response);
        }

        public ActionResult GetSchedules(int facultyId, int courseId, int groupId)
        {
            var studyYear = DataService.GetStudyYear(DateTime.Now);
            var semester = DataService.GetSemesterForTime(DateTime.Now);

            var presentationService = new SchedulePresentationFormatService(); 

            var response = DataService
                .GetSchedulesForGroups(
                    facultyId,
                    courseId,
                    new [] {groupId},
                    studyYear.Id,
                    semester.Id)
                .Select(x => new ScheduleViewModel(x));

            return new JsonNetResult(presentationService.ForGroupFilter(response));
        }
    }
}
