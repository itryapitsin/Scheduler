using System.Web.Mvc;

namespace Timetable.Site.Controllers
{
    [Authorize]
    public class MainController : NewBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
