using System.Configuration;
using System.Messaging;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Context;
using Timetable.Dispatcher;
using Timetable.Dispatcher.Tasks;
using Timetable.Logic.SyncData;

namespace Timetable.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new IIASContext(new OracleConnection(ConfigurationManager.ConnectionStrings["OracleHR"].ConnectionString)))
            {
                var buildings = context.GetBuildings();
            }

            var buildingSync = new BuildingSync();
            buildingSync.IIASContext = new IIASContext(new OracleConnection(ConfigurationManager.ConnectionStrings["OracleHR"].ConnectionString));
            buildingSync.SchedulerDatabase = new SchedulerContext();
            buildingSync.Sync();
            //var task = new SyncDataTask();
            //var alarmTask = new AlarmTask();

            //var publisher = new TaskPublisher();
            ////publisher.Publish(new SyncDataTask());
            ////publisher.Publish(new SyncDataTask());
            //publisher.Publish(task);
            //publisher.Publish(alarmTask);

            //var taskPool = new TaskPool();
            //var t = taskPool.GetTaskById<SyncDataTask>(task.Id);
            //var tasks = taskPool.GetTasks<SyncDataTask>();

            //var reciver = new TaskReciver();
            //reciver.Recive<SyncDataTask>();
        }
    }
}
