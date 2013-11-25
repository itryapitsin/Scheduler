using System.Web.Mvc;

namespace Timetable.Site.Controllers
{
    [Authorize]
    public class MainController : AuthorizedController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
