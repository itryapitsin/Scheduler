using System;
using Timetable.Host.Interfaces;
using Timetable.Sync.Toolkit;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Host.Services
{
    public class DataSyncService : IDataSyncService
    {
        public void DoSync()
        {
            var taskPublisher = new TaskPublisher();
            var task = new SyncDataTask();
            taskPublisher.Publish(task);
        }

        public int GetProgress()
        {
            throw new NotImplementedException();
        }
    }
}
