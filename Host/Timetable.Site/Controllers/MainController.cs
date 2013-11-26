using System.Web.Mvc;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers
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
