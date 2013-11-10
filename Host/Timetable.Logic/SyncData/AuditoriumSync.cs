using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    public class AuditoriumSync: BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetAuditoriums().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Auditoriums.ToList());

            Task.WaitAll(task1, task2);

            var task3 = Task.Factory.StartNew(() => SchedulerDatabase.AuditoriumTypes.ToList());

            Task.WaitAll(task3);

            var iiasEntities = await task1;
            var schedulerEntyties = await task2;
            var schedulerAuditoriumTypes = await task3;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntyty = schedulerEntyties.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var auditoriumType = schedulerAuditoriumTypes.FirstOrDefault(x => iiasEntity.Name.Contains(x.Pattern));
                if(auditoriumType == null)
                    continue;

                if (schedulerEntyty == null)
                {
                    schedulerEntyty = new Auditorium
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        Name = iiasEntity.Name,
                        Number = iiasEntity.Num,
                        BuildingId = iiasEntity.BuildingId,
                        AuditoriumTypeId = auditoriumType.Id,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntyty);
                }
                else
                {
                    schedulerEntyty.UpdatedDate = DateTime.Now;
                    schedulerEntyty.IIASKey = iiasEntity.Id;
                    schedulerEntyty.Name = iiasEntity.Name;
                    schedulerEntyty.Number = iiasEntity.Num;
                    schedulerEntyty.BuildingId = iiasEntity.BuildingId;
                    schedulerEntyty.AuditoriumTypeId = auditoriumType.Id;
                    schedulerEntyty.IsActual = true;

                    SchedulerDatabase.Update(schedulerEntyty);
                }
            }
        }
    }
}
