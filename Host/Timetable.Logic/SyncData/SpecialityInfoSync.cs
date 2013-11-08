using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Base.Entities.Scheduler;

namespace Timetable.Logic.SyncData
{
    public class ScheduleInfoSync : BaseSync
    {
        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetScheduleInfoes().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.ScheduleInfoes.ToList());

            Task.WaitAll(task1, task2);

            var studyYears = SchedulerDatabase.StudyYears.ToList();

            var iiasEntities = await task1;
            var schedulerEntities = await task2;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var years = iiasEntity.StudyYear.Split(new[] { " - ", " / ", "/", "-" }, StringSplitOptions.RemoveEmptyEntries);
                var startYear = int.Parse(years[0]);
                var length = int.Parse(years[1]) - startYear;

                var lecturer = SchedulerDatabase.Lecturers.FirstOrDefault(x => x.IIASKey == iiasEntity.LecturerId);
                var tutorial = SchedulerDatabase.Tutorials.FirstOrDefault(x => x.IIASKey == iiasEntity.TutorialId);
                var tutorialType = SchedulerDatabase.TutorialTypes.FirstOrDefault(x => x.IIASKey == iiasEntity.TutorialTypeId);
                var department = SchedulerDatabase.Departments.FirstOrDefault(x => x.IIASKey == iiasEntity.DepartmentId);
                var studyYear = studyYears.FirstOrDefault(x => x.StartYear == startYear && x.Length == length);
                if (lecturer == null || tutorial == null || tutorialType == null || department == null || studyYear == null)
                    continue;

                if (schedulerEntity == null)
                {
                    schedulerEntity = new ScheduleInfo
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IIASKey = iiasEntity.Id,
                        LecturerId = lecturer.Id,
                        TutorialId = tutorial.Id,
                        TutorialTypeId = tutorialType.Id,
                        DepartmentId = department.Id,
                        StudyYearId = studyYear.Id,
                        Semester = iiasEntity.Semestr,
                        HoursPerWeek = iiasEntity.HoursPerWeek,
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
                else
                {
                    schedulerEntity.UpdatedDate = DateTime.Now;
                    schedulerEntity.IIASKey = iiasEntity.Id;
                    schedulerEntity.LecturerId = lecturer.Id;
                    schedulerEntity.TutorialId = tutorial.Id;
                    schedulerEntity.TutorialTypeId = tutorialType.Id;
                    schedulerEntity.DepartmentId = department.Id;
                    schedulerEntity.StudyYearId = studyYear.Id;
                    schedulerEntity.Semester = iiasEntity.Semestr;
                    schedulerEntity.HoursPerWeek = iiasEntity.HoursPerWeek;

                    SchedulerDatabase.Update(schedulerEntity);
                }
            }
        }
    }
}
