using Microsoft.AspNet.SignalR;
using Timetable.Logic.Services;

namespace Timetable.Site
{
    [Authorize]
    public class DispatcherHub : Hub
    {
        private static string SyncLog;

        public void RefreshData()
        {
            var syncService = new SyncService();

            Clients.Others.refreshDataStarted();

            SendRefreshLog("Запуск синхронизации", false);
            syncService.StepStarted += s => SendRefreshLog(s, false);
            syncService.StepCompleted += s => SendRefreshLog(s, false);
            syncService.SyncDictionaries();
            SendRefreshLog("Синхронизация выполнена", true);

            Clients.Others.refreshDataFinished();
        }

        public void SendRefreshLog(string message, bool isCompleted)
        {
            Clients.All.refreshLog(message, isCompleted);
        }
    }
}