using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация групп")]
    public class GroupSync: BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Groups]
                       ([Code]
                       ,[CourseId]
                       ,[SpecialityId]
                       ,[StudentsCount]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey]
                       ,[Parent_Id]
                       ,[FacultyId])
                 VALUES
                       ('{0}'
                       ,{1}
                       ,{2}
                       ,0
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{3}
                       ,null
                       ,{4});";
        private string _updateQueryPattern = @"
                UPDATE [dbo].[Groups]
                   SET [Code] = '{0}'
                      ,[CourseId] = {1}
                      ,[SpecialityId] = {2}
                      ,[UpdatedDate] = GetDate()
                      ,[FacultyId] = {3}
                 WHERE Id = {4};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetGroups().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Groups.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var faculties = SchedulerDatabase.Faculties.ToList();
            var command = String.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var course = SchedulerDatabase.Courses.FirstOrDefault(x => x.IIASKey == iiasEntity.CourseId);
                var speciality = SchedulerDatabase.Specialities.FirstOrDefault(x => x.IIASKey == iiasEntity.SpecialityId);
                var faculty = faculties.FirstOrDefault(x => x.IIASKey == iiasEntity.FacultyId);
                if(course == null || speciality == null || faculty == null)
                    continue;

                if (schedulerEntity == null)
                {
                    command += string.Format(
                        _insertQueryPattern,
                        iiasEntity.Code,
                        course.Id,
                        speciality.Id,
                        iiasEntity.Id,
                        faculty.Id);
                }
                else
                {
                    command += string.Format(
                        _updateQueryPattern,
                        iiasEntity.Code,
                        course.Id,
                        speciality.Id,
                        faculty.Id,
                        iiasEntity.Id);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
