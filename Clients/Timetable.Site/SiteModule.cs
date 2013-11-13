using Autofac;
using Autofac.Integration.Mvc;
using Timetable.Site.NewDataService;

namespace Timetable.Site
{
    public class SiteModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataServiceClient>().As<IDataService>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }
    }
}