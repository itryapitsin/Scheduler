using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация специальностей")]
    public class SpecialitySync : BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Specialities]
                       ([Name]
                       ,[ShortName]
                       ,[Code]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey]
                       ,[BranchId])
                 VALUES
                       ('{0}'
                       ,'{1}'
                       ,'{2}'
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{3}
                       ,{4});";
        private string _updateQueryPattern = @"
                UPDATE [dbo].[Specialities]
                   SET [Name] ='{0}'
                      ,[ShortName] = '{1}'
                      ,[Code] = '{2}'
                      ,[UpdatedDate] = GetDate()
                      ,[BranchId] = {3}
                 WHERE Id = {4};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetSpecialities().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Specialities.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var branches = SchedulerDatabase.Branches.ToList();
            var command = String.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var branch = branches.FirstOrDefault(x => x.IIASKey == iiasEntity.BranchId);
                if(branch == null)
                    continue;

                if (schedulerEntity == null)
                {
                    command += string.Format(
                        _insertQueryPattern, 
                        iiasEntity.Name,
                        iiasEntity.ShortName,
                        iiasEntity.Code,
                        iiasEntity.Id,
                        branch.Id);
                }
                else
                {
                    command += string.Format(
                        _updateQueryPattern,
                        iiasEntity.Name,
                        iiasEntity.ShortName,
                        iiasEntity.Code,
                        branch.Id,
                        iiasEntity.Id);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
