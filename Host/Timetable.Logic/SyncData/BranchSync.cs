using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Base.Entities.Scheduler;
using Timetable.Base.Interfaces.DataSources;
using Timetable.Data.IIAS.Context;

namespace Timetable.Logic.SyncData
{
    public class BranchSync: BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetBranches().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Branches.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    schedulerEntity = new Branch
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        Name = iiasEntity.Name,
                        OrganizationId = (int)iiasEntity.OrganizationId,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
                else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.IIASKey = iiasEntity.Id;
                    schedulerEntity.Name = iiasEntity.Name;
                    schedulerEntity.OrganizationId = (int)iiasEntity.OrganizationId;
                    schedulerEntity.IsActual = true;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
