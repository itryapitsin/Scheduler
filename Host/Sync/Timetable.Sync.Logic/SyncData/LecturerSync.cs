using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация преподавателей")]
    public class LecturerSync : BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Lecturers]
                       ([Lastname]
                       ,[Firstname]
                       ,[Middlename]
                       ,[Contacts]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey])
                 VALUES
                       ('{0}'
                       ,'{1}'
                       ,'{2}'
                       ,{3}
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{4});";
        private string _updateQueryPattern = @"
                UPDATE [dbo].[Lecturers]
                   SET [Lastname] = '{0}'
                      ,[Firstname] = '{1}'
                      ,[Middlename] = '{2}'
                      ,[Contacts] = '{3}'
                      ,[UpdatedDate] = GetDate()
                 WHERE Id = {4};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetLecturers().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Lecturers.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            var lecturers2 = IIASContext.GetLecturers2().Where(x => !iiasEntities.Select(y => y.Id).Contains(x.Id));
            iiasEntities.AddRange(lecturers2);
            var command = string.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    command += string.Format(
                        _insertQueryPattern,
                        string.IsNullOrEmpty(iiasEntity.Lastname) ? "NULL" : string.Format("{0}", iiasEntity.Lastname),
                        string.IsNullOrEmpty(iiasEntity.Firstname) ? "NULL" : string.Format("{0}", iiasEntity.Firstname),
                        string.IsNullOrEmpty(iiasEntity.Middlename) ? "NULL" : string.Format("{0}", iiasEntity.Middlename),
                        "''",
                        iiasEntity.Id);
                }
                else
                {
                    command += string.Format(
                        _updateQueryPattern,
                         string.IsNullOrEmpty(iiasEntity.Lastname) ? "NULL" : string.Format("{0}", iiasEntity.Lastname),
                        string.IsNullOrEmpty(iiasEntity.Firstname) ? "NULL" : string.Format("{0}", iiasEntity.Firstname),
                        string.IsNullOrEmpty(iiasEntity.Middlename) ? "NULL" : string.Format("{0}", iiasEntity.Middlename),
                         "''",
                        iiasEntity.Id);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
