using System;
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
            Console.Write(@"Loading organizations...");
            DoSync(new OrganizationSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading branches...");
            DoSync(new BranchSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading buildings...");
            DoSync(new BuildingSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading faculties...");
            DoSync(new FacultySync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading departments...");
            DoSync(new DepartmentSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading lecturers...");
            DoSync(new LecturerSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading tutorials...");
            DoSync(new TutorialSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading specialities...");
            DoSync(new SpecialitySync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading tutorial types...");
            DoSync(new TutorialTypeSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading times...");
            DoSync(new TimeSync());
            Console.WriteLine(@"done!");
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
