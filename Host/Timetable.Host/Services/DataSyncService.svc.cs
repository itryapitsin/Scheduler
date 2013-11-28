using System;
using Timetable.Dispatcher;
using Timetable.Dispatcher.Tasks;
using Timetable.Host.Interfaces;

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
