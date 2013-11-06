using System.Web.Mvc;

namespace Timetable.Site.Controllers
{
    /// <summary>
    /// View controller
    /// </summary>
    public class MainController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
