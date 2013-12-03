using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация факультетов")]
    public class FacultySync : BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetFaculties().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Faculties.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var branch = SchedulerDatabase.Branches.FirstOrDefault(x => x.IIASKey == iiasEntity.BranchId);
                if(branch == null)
                    continue;

                if (schedulerEntity == null)
                {
                    schedulerEntity = new Faculty
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        BranchId = branch.Id,
                        Name = iiasEntity.Name,
                        ShortName = iiasEntity.ShortName,
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
                    schedulerEntity.BranchId = branch.Id;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
