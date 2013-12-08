using Autofac;
using Autofac.Integration.Mvc;

namespace Timetable.Site
{
    public class SiteModule: Module
    {
        protected void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }
    }
}