using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoSync : BaseSync
    {
        private string _updatePattern = @"
                UPDATE [dbo].[ScheduleInfoes]
                   SET [LecturerId] = {1}
                      ,[TutorialTypeId] = {2}
                      ,[DepartmentId] = {3}
                      ,[HoursPerWeek] = {4}
                      ,[TutorialId] = {5}
                      ,[StudyYearId] = {6}
                      ,[SemesterId] = {7}
                      ,[IIASKey] = {8}
                      ,[UpdatedDate] = '{9}'
                 WHERE Id = {0};";

        private string _insertPattern = @"
                INSERT INTO [dbo].[ScheduleInfoes]
                           ([LecturerId]
                           ,[TutorialTypeId]
                           ,[DepartmentId]
                           ,[HoursPerWeek]
                           ,[TutorialId]
                           ,[StudyYearId]
                           ,[SemesterId]
                           ,[IsActual]
                           ,[CreatedDate]
                           ,[UpdatedDate]
                           ,[IIASKey]
                           ,[SubgroupCount])
                     VALUES
                           ({0}
                           ,{1}
                           ,{2}
                           ,{3}
                           ,{4}
                           ,{5}
                           ,{6}
                           ,{7}
                           ,'{8}'
                           ,'{9}'
                           ,{10}
                           ,{11});";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetScheduleInfoes().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.ScheduleInfoes.ToList());

            Task.WaitAll(task1, task2);

            var studyYears = SchedulerDatabase.StudyYears.ToList();

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var lecturers = SchedulerDatabase.Lecturers.ToList();
            var tutorials = SchedulerDatabase.Tutorials.ToList();
            var tutorialTypes = SchedulerDatabase.TutorialTypes.ToList();
            var departments = SchedulerDatabase.Departments.ToList();
            var command = String.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var years = iiasEntity.StudyYear.Split(new[] { " - ", " / ", "/", "-" }, StringSplitOptions.RemoveEmptyEntries);
                var startYear = int.Parse(years[0]);
                var length = int.Parse(years[1]) - startYear;

                var lecturer = lecturers.FirstOrDefault(x => x.IIASKey == iiasEntity.LecturerId);
                var tutorial = tutorials.FirstOrDefault(x => x.IIASKey == iiasEntity.TutorialId);
                var tutorialType = tutorialTypes.FirstOrDefault(x => x.IIASKey == iiasEntity.TutorialTypeId);
                var department = departments.FirstOrDefault(x => x.IIASKey == iiasEntity.DepartmentId);
                var studyYear = studyYears.FirstOrDefault(x => x.StartYear == startYear && x.Length == length);
                if (lecturer == null || tutorial == null || tutorialType == null || studyYear == null)
                    continue;
                var semestr = (iiasEntity.Semestr % 2) == 0
                    ? 2
                    : 1;

                if (schedulerEntity == null)
                {
                    command = string.Format(_insertPattern,
                        lecturer.Id,
                        tutorialType.Id,
                        department == null ? "NULL" : department.Id.ToString(),
                        iiasEntity.HoursPerWeek.ToString("g2", CultureInfo.InvariantCulture),
                        tutorial.Id,
                        studyYear.Id,
                        semestr,
                        '1',
                        DateTime.Now.ToString("s"), 
                        DateTime.Now.ToString("s"),
                        iiasEntity.Id,
                        0);
                }
                else
                {
                    command = string.Format(_updatePattern,
                        schedulerEntity.Id,
                        lecturer.Id,
                        tutorialType.Id,
                        department == null ? "NULL" : department.Id.ToString(),
                        iiasEntity.HoursPerWeek.ToString("g2", CultureInfo.InvariantCulture),
                        tutorial.Id,
                        studyYear.Id,
                        semestr,
                        iiasEntity.Id,
                        DateTime.Now.ToString("s"));
                }

                SchedulerDatabase.RawSqlCommand(command);
            }

            
        }
    }
}
