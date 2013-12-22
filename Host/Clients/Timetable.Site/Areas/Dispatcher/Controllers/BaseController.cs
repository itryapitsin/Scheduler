using System.Web.Mvc;
using log4net;
using Timetable.Logic.Services;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class BaseController: Controller
    {
        protected readonly UserService UserService;
        protected readonly ILog Log;

        public BaseController()
        {
            UserService = new UserService();
            Log = LogManager.GetLogger(typeof(Controller));
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            Log.Error(filterContext.Exception.Message, filterContext.Exception);
        }
    }
}