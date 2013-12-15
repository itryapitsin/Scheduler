using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация кафедр")]
    public class DepartmentSync: BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Departments]
                       ([Name]
                       ,[ShortName]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey])
                 VALUES
                       ('{0}'
                       ,'{1}'
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{2});";
        private string _updateQueryPattern = @"
                UPDATE [dbo].[Departments]
                   SET [Name] = '{0}'
                      ,[ShortName] = '{1}'
                      ,[UpdatedDate] = GetDate()
                 WHERE Id = {2};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetDepartments().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Departments.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            var departments2 = IIASContext.GetDepartments2().ToList().Where(x => !iiasEntities.Select(y => y.Id).Contains(x.Id));
            iiasEntities.AddRange(departments2);
            var command = String.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    command += string.Format(
                        _insertQueryPattern,
                        iiasEntity.Name,
                        iiasEntity.ShortName,
                        iiasEntity.Id);
                    //var building = new Department
                    //{
                    //    CreatedDate = DateTime.Now,
                    //    UpdatedDate = DateTime.Now,
                    //    IIASKey = iiasEntity.Id,
                    //    Name = iiasEntity.Name,
                    //    ShortName = iiasEntity.ShortName,
                    //    IsActual = true
                    //};
                    //SchedulerDatabase.Add(building);
                }
                else
                {
                    //schedulerEntity.UpdatedDate = DateTime.Now;
                    //schedulerEntity.IIASKey = iiasEntity.Id;
                    //schedulerEntity.Name = iiasEntity.Name;
                    //schedulerEntity.ShortName = iiasEntity.ShortName;

                    //SchedulerDatabase.Update(schedulerEntity);
                    command += string.Format(
                        _updateQueryPattern,
                        iiasEntity.Name,
                        iiasEntity.ShortName,
                        iiasEntity.Id);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
