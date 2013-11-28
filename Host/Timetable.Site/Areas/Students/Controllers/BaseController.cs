using System.Web.Mvc;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Services;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IDataService DataService;

        public BaseController()
        {
            DataService = new SchedulerService();
        }
    }
}
