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

        public ActionResult RefreshLog()
        {
            var syncService = new SyncService();

            Task.Factory.StartNew(() =>
            {
                GlobalHost.ConnectionManager.GetHubContext<DispatcherHub>().Clients.All.refreshLog("Синхронизация типов расписаний");
            }); 
            Task.Factory.StartNew(() =>
            {
                syncService.DoSync(new ScheduleTypeSync());
                GlobalHost.ConnectionManager.GetHubContext<DispatcherHub>().Clients.All.refreshLog("Синхронизация типов расписаний завершена");
            });

            Task.Factory.StartNew(() =>
            {
                GlobalHost.ConnectionManager.GetHubContext<DispatcherHub>().Clients.All.refreshLog("Синхронизация курсов");
            }); 
            syncService.DoSync(new CourseSync());
            SendRefreshLog("Синхронизация курсов завершена");

            SendRefreshLog("Синхронизация организаций");
            syncService.DoSync(new OrganizationSync());
            SendRefreshLog("Синхронизация организаций завершена");

            SendRefreshLog("Синхронизация филиалов расписаний");
            syncService.DoSync(new BranchSync());
            SendRefreshLog("Синхронизация филиалов завершена");

            SendRefreshLog("Синхронизация предметов расписаний");
            Task.Factory.StartNew(() =>
            {
                syncService.DoSync(new TutorialSync());
                SendRefreshLog("Синхронизация предметов завершена");
            });
            

            return new EmptyResult();
        }

        private void SendRefreshLog(string message)
        {
            //Task.Factory.StartNew(() =>
            //{
                GlobalHost.ConnectionManager.GetHubContext<DispatcherHub>().Clients.All.refreshLog(message);
            //});
        }
    }
}
