using System.Reflection;
using System.Web.Mvc;
using log4net;
using log4net.Core;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Services;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IDataService DataService;
        protected readonly ILog Log;

        public BaseController()
        {
            DataService = new SchedulerService();
            Log = LogManager.GetLogger(typeof(Controller));
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            Log.Error(filterContext.Exception.Message, filterContext.Exception);
        }
    }
}
