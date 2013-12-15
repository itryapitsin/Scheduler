using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация предметов")]
    public class TutorialSync : BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Tutorials]
                       ([Name]
                       ,[ShortName]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey])
                 VALUES
                       ('{0}'
                       ,''
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{1});";
        private string _updateQueryPattern = @"
                UPDATE [dbo].[Tutorials]
                   SET [Name] = '{0}'
                      ,[UpdatedDate] = GetDate()
                 WHERE Id = {1};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetTutorials().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Tutorials.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var command = String.Empty;

            

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    command += string.Format(
                        _insertQueryPattern, 
                        iiasEntity.Name.Replace('\'', '"'),
                        iiasEntity.Id);
                }
                else
                {
                    command += string.Format(
                        _updateQueryPattern,
                        iiasEntity.Name.Replace('\'', '"'),
                        iiasEntity.Id);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
