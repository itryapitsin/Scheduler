using System;
using Timetable.Host.Interfaces;
using Timetable.Sync.Toolkit;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Host.Services
{
    public class DataSyncService : IDataSyncService
    {
        private readonly SyncDataTask _task = new SyncDataTask();

        public void DoSync()
        {
            var taskPublisher = new TaskPublisher();
            taskPublisher.Publish(new SyncDataTask());
        }

        public int GetProgress()
        {
            return _task.GetProgress();
        }
    }
}
