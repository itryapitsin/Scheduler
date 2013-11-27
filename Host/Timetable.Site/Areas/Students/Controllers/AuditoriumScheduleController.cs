using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Students.Models;
using Timetable.Site.Areas.Students.Models.ViewModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class AuditoriumScheduleController : BaseController
    {
        public ActionResult Index(int? auditoriumId = null)
        {
            var model = new AuditoriumScheduleViewModel
            {
                Buildings = DataService
                    .GetBuildings()
                    .Select(x => new BuildingViewModel(x)),

                StudyYears = DataService
                    .GetStudyYears()
                    .Select(x => new StudyYearViewModel(x)),

                Semesters = DataService
                    .GetSemesters()
                    .Select(x => new SemesterViewModel(x)),
            };

            return PartialView("_Index", model);
        }

    }
}
