using System.Web.Mvc;

namespace Timetable.Site.Areas.Students
{
    public class StudentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Students";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Students_default",
                "Students/{controller}/{action}/{id}",
                new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
