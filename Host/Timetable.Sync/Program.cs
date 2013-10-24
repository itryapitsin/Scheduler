using System.Configuration;
using System.Messaging;
using System.Threading.Tasks;
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
            var buildingSyncTask = new Task(buildingSync.Sync);
            buildingSyncTask.Start();
            buildingSyncTask.Wait();
        }
    }
}
