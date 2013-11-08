﻿using System;
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
    public class BuildingSync: BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetBuildings().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Buildings.ToList());

            Task.WaitAll(task1, task2);

            var iiasBuildings = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasBuilding in iiasBuildings)
            {
                var schedulerBuilding = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasBuilding.Id);
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

                    SchedulerDatabase.Update(schedulerBuilding);
                }
            }
        }
    }
}
