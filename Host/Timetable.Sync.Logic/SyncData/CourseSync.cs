using System;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация курсов")]
    public class CourseSync: BaseSync
    {
        public async override void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetCourses().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Courses.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                if (schedulerEntity == null)
                {
                    schedulerEntity = new Course
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsActual = true,
                        Name = iiasEntity.Name,
                        IIASKey = iiasEntity.Id
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                } else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.Name = iiasEntity.Name;
                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
