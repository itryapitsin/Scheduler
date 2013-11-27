using System.Web.Mvc;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class LecturerScheduleController : AuthorizedController
    {
        public ActionResult Index()
        {
            return View("_Index");
        }

        public ActionResult SearchLecturer(string query)
        {
            var lecturers = DataService
                .GetLecturersByFirstMiddleLastname(query)
                .ToEnumerableWithTotal(x => (new LecturerViewModel(x).Name));

            return new JsonNetResult(lecturers);
        }
    }
}
