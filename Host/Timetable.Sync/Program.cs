using System.Configuration;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Context;
using Timetable.Logic.SyncData;

namespace Timetable.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            DoSync(new OrganizationSync());
            DoSync(new BranchSync());
            DoSync(new BuildingSync());
            DoSync(new DepartmentSync());
        }

        public static void DoSync(BaseSync sync)
        {
            sync.IIASContext = new IIASContext(new OracleConnection(ConfigurationManager.ConnectionStrings["OracleHR"].ConnectionString));
            sync.SchedulerDatabase = new SchedulerContext();
            var syncTask = new Task(sync.Sync);
            syncTask.Start();
            syncTask.Wait();
        }
    }
}
