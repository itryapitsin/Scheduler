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

            var iiasBuildings = await task1;
            var schedulerBuildings = await task2;

            foreach (var iiasBuilding in iiasBuildings)
            {
                var schedulerBuilding = schedulerBuildings.FirstOrDefault(x => x.IIASKey == iiasBuilding.Id);
                if (schedulerBuilding == null)
                {
                    var building = new Branch
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasBuilding.Id,
                        Name = iiasBuilding.Name,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(building);
                }
                else
                {
                    schedulerBuilding.UpdatedDate = DateTime.Now;
                    schedulerBuilding.IIASKey = iiasBuilding.Id;
                    schedulerBuilding.Name = iiasBuilding.Name;
                    schedulerBuilding.IsActual = true;

                    SchedulerDatabase.Update(schedulerBuilding);
                }
            }
        }
    }
}
