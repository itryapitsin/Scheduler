using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация аудиторий")]
    public class AuditoriumSync: BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Auditoriums]
                       ([Number]
                       ,[Name]
                       ,[Capacity]
                       ,[Info]
                       ,[BuildingId]
                       ,[AuditoriumTypeId]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey])
                 VALUES
                       ('{0}'
                       ,'{1}'
                       ,{2}
                       ,NULL
                       ,{3}
                       ,{4}
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{5})";
        private string _updateQueryPattern = @"
               UPDATE [dbo].[Auditoriums]
                   SET [Number] = '{0}'
                      ,[Name] = '{1}'
                      ,[Capacity] = {2}
                      ,[Info] = NULL
                      ,[BuildingId] = {3}
                      ,[AuditoriumTypeId] = {4}
                      ,[UpdatedDate] = GetDate()
                 WHERE Id = {5};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetAuditoriums().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Auditoriums.ToList());
           

            Task.WaitAll(task1, task2);

            var task3 = Task.Factory.StartNew(() => SchedulerDatabase.AuditoriumTypes.ToList());
            var task4 = Task.Factory.StartNew(() => IIASContext.GetProductivities().ToList());

            Task.WaitAll(task3, task4);

            var iiasEntities = await task1;
            var schedulerEntyties = await task2;
            var schedulerAuditoriumTypes = await task3;
            var iiasProductivities = await task4;
            var command = string.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntyty = schedulerEntyties.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var auditoriumType = schedulerAuditoriumTypes.FirstOrDefault(x => iiasEntity.Name.Contains(x.Pattern));
                var building = SchedulerDatabase.Buildings.FirstOrDefault(x => x.IIASKey == iiasEntity.BuildingId);
                if(auditoriumType == null || building == null)
                    continue;

                //Может быть долго, но не придумал как перенести эту логику в SQL
                var capacity = iiasProductivities.Where(x => x.Id == iiasEntity.Id).Select(x => x.Capacity).FirstOrDefault();

                if (schedulerEntyty == null)
                {
                    command += string.Format(
                        _insertQueryPattern,
                        iiasEntity.Num,
                        iiasEntity.Name,
                        capacity.HasValue ? capacity.Value : 0,
                        building.Id,
                        auditoriumType.Id,
                        iiasEntity.Id);

                    //schedulerEntyty = new Auditorium
                    //{
                    //    CreatedDate = DateTime.Now,
                    //    UpdatedDate = DateTime.Now,
                    //    IIASKey = iiasEntity.Id,
                    //    Name = iiasEntity.Name,
                    //    Number = iiasEntity.Num,
                    //    BuildingId = building.Id,
                    //    AuditoriumTypeId = auditoriumType.Id,
                    //    IsActual = true
                    //};
                    //SchedulerDatabase.Add(schedulerEntyty);
                    
                }
                else
                {
                    //Обновление аудиторий странно работает наверно последний параметр не  iiasEntity.Id, а Id в MS SQL Server, т. к. они не совпадают.
                    command += string.Format(
                        _updateQueryPattern,
                        iiasEntity.Num,
                        iiasEntity.Name,
                        capacity.HasValue ? capacity.Value : 0,
                        building.Id,
                        auditoriumType.Id,
                        iiasEntity.Id);

                    //schedulerEntyty.UpdatedDate = DateTime.Now;
                    //schedulerEntyty.IIASKey = iiasEntity.Id;
                    //schedulerEntyty.Name = iiasEntity.Name;
                    //schedulerEntyty.Number = iiasEntity.Num;
                    //schedulerEntyty.BuildingId = building.Id;
                    //schedulerEntyty.AuditoriumTypeId = auditoriumType.Id;
                    //schedulerEntyty.IsActual = true;

                    //SchedulerDatabase.Update(schedulerEntyty);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
