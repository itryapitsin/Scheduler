using System.Threading.Tasks;
using System.Web.Mvc;
using Timetable.Logic.Services;
using Microsoft.AspNet.SignalR;
using Timetable.Sync.Logic.SyncData;

namespace Timetable.Site.Controllers
{
    public class SettingsController : AuthorizedController
    {
        public ActionResult Index()
        {
            return View("_Index");
        }
    }
}
