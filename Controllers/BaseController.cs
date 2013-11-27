using System.Web.Mvc;
using Timetable.Logic.Services;

namespace Timetable.Site.Controllers
{
    public class BaseController: Controller
    {
        protected readonly UserService UserService;

        public BaseController()
        {
            UserService = new UserService();
        }
    }
}