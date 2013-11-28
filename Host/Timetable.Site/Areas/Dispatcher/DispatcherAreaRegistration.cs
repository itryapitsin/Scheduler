using System.Web.Mvc;

namespace Timetable.Site.Areas.Dispatcher
{
    public class DispatcherAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Dispatcher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Dispatcher_default",
                "Dispatcher/{controller}/{action}/{id}",
                new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
