using System.Web.Mvc;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    [Authorize]
    public class MainController : AuthorizedController
    {
        public ActionResult Index()
        {
            var model = new PageViewModel
            {
                UserName = User.Identity.Name, 
                UserType = UserData.Type
            };
            return View(model);
        }
    }
}
