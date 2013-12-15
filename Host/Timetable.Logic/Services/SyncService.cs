using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Context;
using Timetable.Sync.Logic.SyncData;
using System.ComponentModel;

namespace Timetable.Logic.Services
{
    public class SyncService
    {
        private OracleConnection _iiasConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleHR"].ConnectionString);

        public event Action<string> StepCompleted;
        public event Action<string> StepStarted;

        protected virtual void OnStepStarted(string obj)
        {
            foreach (Delegate handler in StepStarted.GetInvocationList())
                handler.DynamicInvoke(obj);
        }

        protected virtual void OnStepComplete(string obj)
        {
            Action<string> handler = StepCompleted;
            if (handler != null) handler(obj);
        }


        public void DoSync(BaseSync sync)
        {
            var description = sync.GetType().GetCustomAttributes(typeof(DescriptionAttribute)).FirstOrDefault() as DescriptionAttribute;

            if (description != null)
                OnStepStarted("Начинает выполняться: " + description.Description);
            else
                OnStepStarted("Начинает выполняться новая задача");

            sync.IIASContext = new IIASContext(_iiasConnection);
            sync.SchedulerDatabase = new SchedulerContext();
            var syncTask = new Task(sync.Sync);
            syncTask.Start();
            syncTask.Wait();

            if (description != null)
                OnStepComplete("Выполнено: " + description.Description);
            else
                OnStepComplete("Задача выполнена");
        }

        public void SyncDictionaries()
        {
            DoSync(new ScheduleTypeSync());
            DoSync(new StudyTypeSync());
            DoSync(new CourseSync());
            DoSync(new StudyYearSync(_iiasConnection));
            DoSync(new WeekTypeSync(_iiasConnection));
            DoSync(new OrganizationSync());
            DoSync(new BranchSync());
            DoSync(new BuildingSync());
            DoSync(new AuditoriumSync());
            DoSync(new FacultySync());
            DoSync(new DepartmentSync());
            DoSync(new LecturerSync());
            DoSync(new TutorialSync());
            DoSync(new SpecialitySync());
            DoSync(new GroupSync());
            DoSync(new BranchesToCoursesSync(_iiasConnection));
            DoSync(new DepartmentsToFacultiesSync(_iiasConnection));
            DoSync(new FacultiesToTutorialsSync(_iiasConnection));
            DoSync(new GroupsToFacultiesSync(_iiasConnection));
            DoSync(new GroupsToCoursesSync(_iiasConnection));
            DoSync(new TutorialTypeSync());
            DoSync(new TimeSync());
            DoSync(new ScheduleInfoSync());
            DoSync(new ScheduleInfoesToFacultiesSync(_iiasConnection));
            DoSync(new ScheduleInfoesToCoursesSync(_iiasConnection));
            DoSync(new ScheduleInfoesToGroupsSync(_iiasConnection));
            DoSync(new ScheduleInfoesToSpecialitiesSync(_iiasConnection));
            DoSync(new SpecialitiesToFacultiesSync(_iiasConnection));
            DoSync(new SpecialitiesToTutorialsSync(_iiasConnection));
        }
    }
}
