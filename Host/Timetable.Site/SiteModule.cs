using Autofac;
using Autofac.Integration.Mvc;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Services;


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