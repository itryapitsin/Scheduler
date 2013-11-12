using Autofac;
using Autofac.Integration.Mvc;
using Timetable.Site.NewDataService;

namespace Timetable.Site
{
    public class SiteModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IDataService>().As<DataServiceClient>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }
    }
}