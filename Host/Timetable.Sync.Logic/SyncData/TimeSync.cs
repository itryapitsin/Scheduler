using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация времен звонков")]
    public class TimeSync : BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetTimes().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Times.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    schedulerEntity = new Time
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        Start = TimeSpan.Parse(iiasEntity.Start),
                        End = TimeSpan.Parse(iiasEntity.Finish),
                        Position = (int)iiasEntity.Position,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
                else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.IIASKey = iiasEntity.Id;
                    schedulerEntity.Start = TimeSpan.Parse(iiasEntity.Start);
                    schedulerEntity.End = TimeSpan.Parse(iiasEntity.Finish);
                    schedulerEntity.Position = (int) iiasEntity.Position;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
