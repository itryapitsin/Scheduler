using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    public class GroupSync: BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetGroups().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Groups.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var faculties = SchedulerDatabase.Faculties.ToList();

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var course = SchedulerDatabase.Courses.FirstOrDefault(x => x.IIASKey == iiasEntity.CourseId);
                var speciality = SchedulerDatabase.Specialities.FirstOrDefault(x => x.IIASKey == iiasEntity.SpecialityId);
                var faculty = faculties.FirstOrDefault(x => x.IIASKey == iiasEntity.FacultyId);
                if(course == null || speciality == null || faculty == null)
                    continue;

                if (schedulerEntity == null)
                {
                    schedulerEntity = new Group
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        Code = iiasEntity.Code,
                        SpecialityId = speciality.Id,
                        CourseId = course.Id,
                        FacultyId = faculty.Id,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
                else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.Code = iiasEntity.Code;
                    schedulerEntity.FacultyId = faculty.Id;
                    schedulerEntity.CourseId = course.Id;
                    schedulerEntity.SpecialityId = speciality.Id;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
