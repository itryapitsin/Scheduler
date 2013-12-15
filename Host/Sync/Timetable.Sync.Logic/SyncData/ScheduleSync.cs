using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleSync: BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetSchedules().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Schedules.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var scheduleInfo = SchedulerDatabase.ScheduleInfoes.FirstOrDefault(x => x.IIASKey == iiasEntity.ScheduleInfoId);
                var weekType = SchedulerDatabase.WeekTypes.FirstOrDefault(x => x.Name == iiasEntity.WeekType);
                var auditorium = SchedulerDatabase.Auditoriums.FirstOrDefault(x => x.IIASKey == iiasEntity.AuditoriumId);
                var scheduleType = SchedulerDatabase.ScheduleTypes.FirstOrDefault(x => x.IIASKey == iiasEntity.ScheduleTypeId);
                var time = SchedulerDatabase.Times.FirstOrDefault(x => x.IIASKey == iiasEntity.TimeId);

                if(scheduleInfo == null || weekType == null || auditorium == null || time == null)
                    continue;

                if (schedulerEntity == null)
                {
                    schedulerEntity = new Schedule
                    {
                        ScheduleInfoId = scheduleInfo.Id,
                        WeekTypeId = weekType.Id,
                        AuditoriumId = auditorium.Id,
                        TypeId = scheduleType != null ? (int?)scheduleType.Id : null,
                        StartDate = iiasEntity.DateStart,
                        EndDate = iiasEntity.DateEnd,
                        TimeId = time.Id,
                        DayOfWeek = (int)iiasEntity.DateStart.DayOfWeek,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
                else
                {
                    schedulerEntity.ScheduleInfoId = scheduleInfo.Id;
                    schedulerEntity.WeekTypeId = weekType.Id;
                    schedulerEntity.AuditoriumId = auditorium.Id;
                    schedulerEntity.TypeId = scheduleType != null ? (int?)scheduleType.Id : null;
                    schedulerEntity.StartDate = iiasEntity.DateStart;
                    schedulerEntity.EndDate = iiasEntity.DateEnd;
                    schedulerEntity.TimeId = time.Id;
                    schedulerEntity.DayOfWeek = (int) iiasEntity.DateStart.DayOfWeek;
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.IIASKey = iiasEntity.Id;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
