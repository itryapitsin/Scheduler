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
                      ,[Semester] = {7}
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
                           ,[Semester]
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
                if (lecturer == null || tutorial == null || tutorialType == null || department == null || studyYear == null)
                    continue;

                if (schedulerEntity == null)
                {
                    command += string.Format(_insertPattern,
                        lecturer.Id,
                        tutorialType.Id,
                        department.Id,
                        iiasEntity.HoursPerWeek.ToString("g2", CultureInfo.InvariantCulture),
                        tutorial.Id,
                        studyYear.Id,
                        iiasEntity.Semestr,
                        '1',
                        DateTime.Now.ToString("s"), 
                        DateTime.Now.ToString("s"),
                        iiasEntity.Id,
                        0);

                    //schedulerEntity = new ScheduleInfo
                    //{
                    //    CreatedDate = DateTime.Now,
                    //    UpdatedDate = DateTime.Now,
                    //    IIASKey = iiasEntity.Id,
                    //    LecturerId = lecturer.Id,
                    //    TutorialId = tutorial.Id,
                    //    TutorialTypeId = tutorialType.Id,
                    //    DepartmentId = department.Id,
                    //    StudyYearId = studyYear.Id,
                    //    Semester = iiasEntity.Semestr,
                    //    HoursPerWeek = iiasEntity.HoursPerWeek,
                    //    IsActual = true
                    //};
                    //SchedulerDatabase.Add(schedulerEntity, false);
                }
                else
                {
                    command += string.Format(_updatePattern,
                        schedulerEntity.Id,
                        lecturer.Id,
                        tutorialType.Id,
                        department.Id,
                        iiasEntity.HoursPerWeek.ToString("g2", CultureInfo.InvariantCulture),
                        tutorial.Id,
                        studyYear.Id,
                        iiasEntity.Semestr,
                        iiasEntity.Id,
                        DateTime.Now.ToString("s"));

                    //schedulerEntity.UpdatedDate = DateTime.Now;
                    //schedulerEntity.IIASKey = iiasEntity.Id;
                    //schedulerEntity.LecturerId = lecturer.Id;
                    //schedulerEntity.TutorialId = tutorial.Id;
                    //schedulerEntity.TutorialTypeId = tutorialType.Id;
                    //schedulerEntity.DepartmentId = department.Id;
                    //schedulerEntity.StudyYearId = studyYear.Id;
                    //schedulerEntity.Semester = iiasEntity.Semestr;
                    //schedulerEntity.HoursPerWeek = iiasEntity.HoursPerWeek;

                    //SchedulerDatabase.Update(schedulerEntity, false);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
