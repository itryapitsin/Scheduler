using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Base.Entities.Scheduler;

namespace Timetable.Logic.SyncData
{
    public class LecturerSync: BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetLecturers().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Lecturers.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    schedulerEntity = new Lecturer
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        Firstname = iiasEntity.Firstname,
                        Middlename = iiasEntity.Middlename,
                        Lastname = iiasEntity.Lastname,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity, false);
                }
                else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.IIASKey = iiasEntity.Id;
                    schedulerEntity.Firstname = iiasEntity.Firstname;
                    schedulerEntity.Middlename = iiasEntity.Middlename;
                    schedulerEntity.Lastname = iiasEntity.Lastname;
                    schedulerEntity.IsActual = true;

                    SchedulerDatabase.Update(schedulerEntity, false);
                }
            }

            SchedulerDatabase.SaveChanges();
        }
    }
}
