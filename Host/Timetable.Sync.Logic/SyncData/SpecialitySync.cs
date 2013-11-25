﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    public class SpecialitySync : BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetSpecialities().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Specialities.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var branches = SchedulerDatabase.Branches.ToList();

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var branch = branches.FirstOrDefault(x => x.IIASKey == iiasEntity.BranchId);
                if(branch == null)
                    continue;

                if (schedulerEntity == null)
                {
                    schedulerEntity = new Speciality
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        Name = iiasEntity.Name,
                        ShortName = iiasEntity.ShortName,
                        Code = iiasEntity.Code,
                        BranchId = branch.Id,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
                else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.IIASKey = iiasEntity.Id;
                    schedulerEntity.Name = iiasEntity.Name;
                    schedulerEntity.ShortName = iiasEntity.ShortName;
                    schedulerEntity.Code = iiasEntity.Code;
                    schedulerEntity.BranchId = branch.Id;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}