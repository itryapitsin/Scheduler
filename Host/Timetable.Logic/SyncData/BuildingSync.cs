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
    public class BuildingSync
    {
        public IIIASContext IIASContext;

        public ISchedulerDatabase SchedulerDatabase;

        public async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetBuildings().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Buildings.ToList());

            Task.WaitAll(task1, task2);

            var iiasBuildings = await task1;
            var schedulerBuildings = await task2;

            foreach (var iiasBuilding in iiasBuildings)
            {
                var schedulerBuilding = schedulerBuildings.FirstOrDefault(x => x.IIASKey == iiasBuilding.Id);
                if (schedulerBuilding == null)
                {
                    var building = new Building
                    {
                        Address = iiasBuilding.Address,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasBuilding.Id,
                        Name = iiasBuilding.Fullname,
                        ShortName = iiasBuilding.ShortName,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(building);
                }
                else
                {
                    schedulerBuilding.Address = iiasBuilding.Address;
                    schedulerBuilding.UpdatedDate = DateTime.Now;
                    schedulerBuilding.IIASKey = iiasBuilding.Id;
                    schedulerBuilding.Name = iiasBuilding.Fullname;
                    schedulerBuilding.ShortName = iiasBuilding.ShortName;
                    schedulerBuilding.IsActual = true;

                    SchedulerDatabase.Update(schedulerBuilding);
                }
            }
        }
    }
}
