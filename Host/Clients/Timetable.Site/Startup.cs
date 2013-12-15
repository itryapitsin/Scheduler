using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Timetable.Site.Startup))]

namespace Timetable.Site
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
