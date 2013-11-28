using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Students.Models.ViewModels;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class ThreadScheduleController : BaseController
    {
        public ActionResult Index()
        {
            var branches = DataService
                .GetBranches()
                .Select(x => new BranchViewModel(x));

            var model = new ThreadScheduleViewModel
            {
                Branches = branches,
            };

            return View("_Index", model);
        }

    }
}
