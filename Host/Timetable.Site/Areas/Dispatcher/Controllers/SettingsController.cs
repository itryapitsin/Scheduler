using System.Web.Mvc;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SettingsController : AuthorizedController
    {
        public ActionResult Index()
        {
            return View("_Index");
        }
    }
}
