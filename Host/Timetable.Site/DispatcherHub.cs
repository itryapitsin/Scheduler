using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Timetable.Logic.Services;
using Timetable.Sync.Logic.SyncData;

namespace Timetable.Site
{
    [Authorize]
    public class DispatcherHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void RefreshData()
        {
            var syncService = new SyncService();

            Clients.Others.refreshDataStarted();

            SendRefreshLog("Началась синхронизация типов расписаний", false);
            syncService.DoSync(new ScheduleTypeSync());
            SendRefreshLog("Синхронизация типов расписаний завершена", false);

            SendRefreshLog("Началась синхронизация курсов", false);
            syncService.DoSync(new CourseSync());
            SendRefreshLog("Синхронизация курсов завершена", false);

            SendRefreshLog("Началась синхронизация организаций", false);
            syncService.DoSync(new OrganizationSync());
            SendRefreshLog("Синхронизация организаций завершена", false);

            SendRefreshLog("Началась синхронизация филиалов", false);
            syncService.DoSync(new BranchSync());
            SendRefreshLog("Синхронизация филиалов завершена", false);

            SendRefreshLog("Началась синхронизация кафедр", false);
            syncService.DoSync(new DepartmentSync());
            SendRefreshLog("Синхронизация кафедр завершена", false);
            SendRefreshLog("Вся синхронизация выполнена", true);

            Clients.Others.refreshDataFinished();
        }

        public void SendRefreshLog(string message, bool isCompleted)
        {
            Clients.All.refreshLog(message, isCompleted);
        }
    }
}