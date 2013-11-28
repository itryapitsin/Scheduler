using System;
using System.Configuration;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Context;
using Timetable.Sync.Logic.SyncData;

namespace Timetable.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new OracleConnection("DATA SOURCE=iias.karelia.ru:1521/iias;USER ID=DPYATIN;Password=xgmst321");

            Console.Write(@"Loading schedule types...");
            DoSync(new ScheduleTypeSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading courses...");
            DoSync(new CourseSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading study years...");
            DoSync(new StudyYearSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading week types...");
            DoSync(new WeekTypeSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading organizations...");
            DoSync(new OrganizationSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading branches...");
            DoSync(new BranchSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading buildings...");
            DoSync(new BuildingSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading auditoriums...");
            DoSync(new AuditoriumSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading faculties...");
            DoSync(new FacultySync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading departments...");
            DoSync(new DepartmentSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading departments to faculties...");
            DoSync(new DepartmentsToFacultiesSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading lecturers...");
            DoSync(new LecturerSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading tutorials...");
            DoSync(new TutorialSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading faculties to tutorials...");
            DoSync(new FacultiesToTutorialsSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading specialities...");
            DoSync(new SpecialitySync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading groups...");
            DoSync(new GroupSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading tutorial types...");
            DoSync(new TutorialTypeSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading times...");
            DoSync(new TimeSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading schedule infoes...");
            DoSync(new ScheduleInfoSync());
            Console.WriteLine(@"done!");

            Console.Write(@"Loading schedule infoes to faculties...");
            DoSync(new ScheduleInfoesToFacultiesSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading schedule infoes to courses...");
            DoSync(new ScheduleInfoesToCoursesSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading schedule infoes to groups...");
            DoSync(new ScheduleInfoesToGroupsSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading schedule infoes to specialities...");
            DoSync(new ScheduleInfoesToSpecialitiesSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading specialities to faculties...");
            DoSync(new SpecialitiesToFacultiesSync(conn));
            Console.WriteLine(@"done!");

            Console.Write(@"Loading specialities to tutorials...");
            DoSync(new SpecialitiesToTutorialsSync(conn));
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
