using System.Linq;
using System.Web.Mvc;
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
                .GetGroupsForFaculty(facultyId, courseId)
                .Select(x => new GroupViewModel(x));

            return new JsonNetResult(response);
        }


    }
}
