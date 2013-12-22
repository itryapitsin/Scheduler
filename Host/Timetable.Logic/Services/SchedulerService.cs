using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Timetable.Data.Migrations;
using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Exceptions;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Logic.Services
{
    public class SchedulerService : BaseService, IDataService
    {
        public bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer)
        {
            var schedulesCount = 0;
            var schedules = Database.Schedules
                .Where(x => x.IsActual
                        && x.AuditoriumId == scheduleDataTransfer.Auditorium.Id
                        && x.TimeId == scheduleDataTransfer.Time.Id
                        && x.DayOfWeek == scheduleDataTransfer.DayOfWeek
                        && (x.StartDate >= scheduleDataTransfer.StartDate && x.StartDate <= scheduleDataTransfer.EndDate
                        || x.EndDate >= scheduleDataTransfer.StartDate && x.EndDate <= scheduleDataTransfer.EndDate));


            if (scheduleDataTransfer.WeekType.Id == 1)
                schedulesCount = schedules.Count();

            if (scheduleDataTransfer.WeekType.Id == 2)
                schedulesCount = schedules.Count(x => x.WeekTypeId == 1 || x.WeekTypeId == 2);

            if (scheduleDataTransfer.WeekType.Id == 3)
                schedulesCount = schedules.Count(x => x.WeekTypeId == 1 || x.WeekTypeId == 3);

            return schedulesCount == 0;
        }

        public IEnumerable<SemesterDataTransfer> GetSemesters()
        {
            return Database.Semesters
                .ToList()
                .Select(x => new SemesterDataTransfer(x));
        }

        public IEnumerable<BranchDataTransfer> GetBranches()
        {
            return Database.Branches
                .Where(x => x.IsActual)
                .ToList()
                .Select(x => new BranchDataTransfer(x));
        }

        public AuditoriumDataTransfer GetAuditoriumById(int auditoriumId) 
        {
            return Database.Auditoriums
                  .Where(x => x.Id == auditoriumId)
                  .Include(x => x.Building)
                  .Include(x => x.AuditoriumType)
                  .ToList()
                  .Select(x => new AuditoriumDataTransfer(x))
                  .FirstOrDefault();
        }

        public IEnumerable<AuditoriumDataTransfer> GetAuditoriums(
            int buildingId,
            int[] auditoriumTypeIds = null,
            bool? isTraining = null)
        {
            var auditoriums = Database.Auditoriums
                .Where(x => x.Building.Id == buildingId)
                .Where(x => x.IsActual)
                .Include(x => x.Building)
                .Include(x => x.AuditoriumType);

            if (auditoriumTypeIds != null && auditoriumTypeIds.Count() > 0)
                auditoriums = auditoriums.Where(x => auditoriumTypeIds.Contains(x.AuditoriumType.Id));

            if (isTraining.HasValue)
                auditoriums = auditoriums.Where(x => x.AuditoriumType.Training == isTraining.Value);

            return auditoriums
                .OrderBy(x => x.Number)
                .ToList()
                .Select(x => new AuditoriumDataTransfer(x));
        }

        public IEnumerable<AuditoriumDataTransfer> GetFreeAuditoriums(
            int buildingId,
            int dayOfWeek,
            int weekTypeId,
            int pair)
        {
            var time = Database.Times.FirstOrDefault(x => x.Position == pair && x.Buildings.Any(y => y.Id == buildingId));

            var scheduledAuditoriums = Database.Schedules
                .Where(x => x.IsActual)
                .Where(x => x.Time.Id == time.Id)
                .Where(x => x.DayOfWeek == dayOfWeek)
                .Where(x => x.WeekType.Id == weekTypeId)
                .Select(x => x.Auditorium);

            var freeAuditoriums = Database.Auditoriums
                .Where(x => x.IsActual)
                .Where(x => x.BuildingId == buildingId)
                .Include(x => x.Building)
                .Include(x => x.AuditoriumType)
                .Except(scheduledAuditoriums)
                .OrderBy(x => x.Number)
                .ToList()
                .Select(x => new AuditoriumDataTransfer(x));

            return freeAuditoriums;
        }

        public IEnumerable<BuildingDataTransfer> GetBuildings()
        {
            var result = Database.Buildings.ToList().Select(x => new BuildingDataTransfer(x));
            return result;
        }

        public IEnumerable<int> GetPairs()
        {
            var result = Database.Times.Select(x => x.Position).Distinct();
            return result;
        }

        public IEnumerable<CourseDataTransfer> GetCources(int branchId)
        {
            return Database.Courses
                .Where(x => x.Branches.Any(y => y.Id == branchId))
                .Where(x => x.IsActual)
                .ToList()
                .Select(x => new CourseDataTransfer(x));
        }


        public IEnumerable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null)
        {
            if (branchDataTransfer == null)
                return Database.Faculties
                    .Where(x => x.IsActual)
                    .Where(x => x.Branch == null)
                    .ToList()
                    .Select(x => new FacultyDataTransfer(x));

            return Database.Faculties
                .Where(x => x.IsActual)
                .Where(x => x.BranchId == branchDataTransfer.Id)
                .ToList()
                .Select(x => new FacultyDataTransfer(x));
        }

        public IEnumerable<FacultyDataTransfer> GetFaculties(int branchId)
        {
            return Database.Faculties
                .Where(x => x.IsActual)
                .Where(x => x.BranchId == branchId)
                .ToList()
                .Select(x => new FacultyDataTransfer(x));
        }

        #region groups
        public IEnumerable<GroupDataTransfer> GetGroupsByIds(int[] groupIds)
        {
            return Database.Groups
                .Where(x => groupIds.Contains(x.Id))
                .ToList()
                .Select(x => new GroupDataTransfer(x));
        }

        public IEnumerable<GroupDataTransfer> GetGroupsByCode(string code, int count)
        {
            return Database.Groups
                .Where(x => x.Code.Contains(code))
                .Take(count)
                .ToList()
                .Select(x => new GroupDataTransfer(x));
        }

        public IEnumerable<GroupDataTransfer> GetsSubGroupsByGroupId(int groupId)
        {
            return Database.Groups
                .Where(x => x.Parent.Id.Equals(groupId))
                .ToList()
                .Select(x => new GroupDataTransfer(x));
        }

        public IEnumerable<GroupDataTransfer> GetGroupsForFaculty(int facultyId, int courseId)
        {
            var result = Database.Groups
                .Where(x => x.IsActual)
                .Where(x => x.Courses.Any(y => y.Id == courseId))
                .Where(x => x.Faculties.Any(y => y.Id == facultyId))
                .ToList()
                .Select(x => new GroupDataTransfer(x));

            return result;
        }

        public IEnumerable<GroupDataTransfer> GetGroupsForFaculty(int facultyId, int courseId, int studyTypeId)
        {
            var result = Database.Groups
                .Where(x => x.IsActual)
                .Where(x => x.Courses.Any(y => y.Id == courseId))
                .Where(x => x.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.StudyTypeId == studyTypeId)
                .ToList()
                .Select(x => new GroupDataTransfer(x));

            return result;
        }

        #endregion

        #region lecturers
        public IEnumerable<LecturerDataTransfer> GetLecturersByDeparmentId(int departmentId)
        {
            return Database.Lecturers
                .Where(x => x.Departments
                    .Any(y => y.Id.Equals(departmentId)))
                .ToList()
                .Select(x => new LecturerDataTransfer(x));
        }

        public LecturerDataTransfer GetLecturerBySearchQuery(string queryString)
        {
            queryString = queryString.Replace(".", "");
            var list = queryString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var lastName = list[0];
            var firstName = "";
            var middleName = "";

            if (list.Length >= 2) firstName = list[1];
            if (list.Length >= 3) middleName = list[2];

            var query = Database.Lecturers.Where(x => x.Lastname == lastName &&
                 x.Middlename.StartsWith(middleName) &&
                 x.Firstname.StartsWith(firstName))
                .ToList()
                .Select(x => new LecturerDataTransfer(x));

            return query.FirstOrDefault();
        }

        public LecturerDataTransfer GetLecturerById(int lecturerId)
        {
            return Database.Lecturers
                .Where(x => x.Id == lecturerId).ToList()
                .Select(x => new LecturerDataTransfer(x))
                .FirstOrDefault();
        }

        public IEnumerable<LecturerDataTransfer> GetLecturersByTutorialId(int tutorialId)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Tutorial.Id.Equals(tutorialId))
                .ToList()
                .Select(x => new LecturerDataTransfer(x.Lecturer));
        }

        public IEnumerable<LecturerDataTransfer> GetLecturersByTutorialIdAndTutorialTypeId(
            int tutorialId,
            int tutorialTypeId)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Tutorial.Id.Equals(tutorialId))
                .Where(x => x.TutorialType.Id.Equals(tutorialTypeId))
                .ToList()
                .Select(x => new LecturerDataTransfer(x.Lecturer));
        }

        public LecturerDataTransfer GetLecturerByFirstMiddleLastname(string arg)
        {
            return GetLecturersByFirstMiddleLastname(arg)
                .FirstOrDefault();
        }

        public IEnumerable<LecturerDataTransfer> GetLecturersByFirstMiddleLastname(string arg)
        {
            var match = Regex.Match(arg, "[а-яА-Я]*");

            var result = new List<LecturerDataTransfer>();

            while (match.Success)
            {
                if (!string.IsNullOrEmpty(match.Value))
                {
                    var query = Database.Lecturers
                        .Where(x => x.Lastname.Contains(match.Value)
                                    || x.Middlename.Contains(match.Value)
                                    || x.Firstname.Contains(match.Value))
                        .Include(x => x.Departments)
                        .ToList()
                        .Select(x => new LecturerDataTransfer(x));

                    result.AddRange(query);
                }

                match = match.NextMatch();
            }

            return result.AsQueryable();
        }
        #endregion

        public ScheduleInfoDataTransfer GetScheduleInfoById(int id)
        {
            return new ScheduleInfoDataTransfer(
                GetScheduleInfoes().FirstOrDefault(scheduleInfo => scheduleInfo.Id == id));
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForFaculty(
            int facultyId,
            int courseId,
            int studyYear,
            int semester)
        {
            return Database.ScheduleInfoes
                .Where(x => x.StudyYear.Id == studyYear)
                .Where(x => x.SemesterId == semester)
                .Where(x => x.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.Courses.Any(y => y.Id == courseId))
                .Include(x => x.Lecturer)
                .Include(x => x.Tutorial)
                .Include(x => x.TutorialType)
                //.Include(x => x.Groups)
                .ToList()
                .Select(x => new ScheduleInfoDataTransfer(x));
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroups(
            int[] groupIds,
            int studyYear,
            int semester)
        {
            return Database.ScheduleInfoes
                .Where(x => x.StudyYearId == studyYear)
                .Where(x => x.SemesterId == semester)
                .Where(x => x.Groups.Any(y => groupIds.Contains(y.Id)))
                .Include(x => x.Lecturer)
                .Include(x => x.Tutorial)
                .Include(x => x.TutorialType)
                //.Include(x => x.Groups)
                .ToList()
                .Select(x => new ScheduleInfoDataTransfer(x));
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoes(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester)
        {
            if (groupIds.Count() > 0)
                return GetScheduleInfoesForGroups(groupIds, studyYear, semester);
            return GetScheduleInfoesForFaculty(facultyId, courseId, studyYear, semester);
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetUnscheduledInfoes(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            GroupDataTransfer groupDataTransfer)
        {
            var test = GetScheduleInfoes()
                .Where(x => x.Schedules.Count == 0)
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
                .Where(x => x.Groups.Any(y => y.Id == groupDataTransfer.Id));

            return test.ToList().Select(x => new ScheduleInfoDataTransfer(x));
        }

        private IEnumerable<ScheduleInfo> GetScheduleInfoes()
        {
            return Database.ScheduleInfoes
                .Include(x => x.Lecturer)
                .Include(x => x.Tutorial)
                .Include(x => x.TutorialType)
                .Include(x => x.Courses)
                .Include(x => x.Groups)
                .Include(x => x.Faculties)
                .Include(x => x.Specialities);
        }

        public IEnumerable<AuditoriumTypeDataTransfer> GetAuditoriumTypes(bool? isTraining = null)
        {
            var result = Database.AuditoriumTypes.AsQueryable();

            if (isTraining.HasValue)
                result = result.Where(x => x.Training == isTraining.Value);

            return result
                .ToList()
                .Select(x => new AuditoriumTypeDataTransfer(x));
        }

        #region schedules

        public int CountScheduleCollisions(
            int day,
            int timeId,
            int weekTypeId)
        {
            var weekType = Database.WeekTypes
                .Where(x => x.IsActual)
                .FirstOrDefault(x => x.Name == "Л");

            var result = Database.Schedules
                .Where(x => x.IsActual)
                .Where(x => x.TimeId == timeId)
                .Where(x => x.DayOfWeek == day)
                .Where(x => ((x.WeekTypeId == weekTypeId) || (x.WeekTypeId == weekType.Id)));
            return result.Count();
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedules(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester)
        {
            if (groupIds.Count() > 0)
                return GetSchedulesForGroups(facultyId, courseId, groupIds, studyYear, semester);
            return GetSchedulesForFaculty(facultyId, courseId, studyYear, semester);
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedules(
            int[] auditoriumIds,
            int studyYear,
            int semester)
        {
            if (auditoriumIds.Count() == 0)
                return Enumerable.Empty<ScheduleDataTransfer>();

            var result = GetSchedules()
                .Where(x => auditoriumIds.Any(y => y == x.AuditoriumId))
                .Where(x => x.ScheduleInfo.StudyYearId == studyYear)
                .Where(x => x.ScheduleInfo.SemesterId == semester);

            return result.ToList().Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedules(
            int auditoriumTypeId,
            int studyYear,
            int semester)
        {
            var result = GetSchedules()
                .Where(x => x.Auditorium.AuditoriumTypeId == auditoriumTypeId)
                .Where(x => x.ScheduleInfo.StudyYearId == studyYear)
                .Where(x => x.ScheduleInfo.SemesterId == semester);

            return result.ToList().Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForFaculty(
            int facultyId,
            int courseId,
            int studyYearId,
            int semester)
        {
            var result = GetSchedules()
                .Where(x => x.IsActual)
                .Where(x => x.ScheduleInfo.StudyYearId == studyYearId)
                .Where(x => x.ScheduleInfo.SemesterId == semester)
                .Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.ScheduleInfo.Courses.Any(y => y.Id == courseId))
                .OrderByDescending(x => x.WeekTypeId);

            return result.ToList().Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForGroups(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYearId == studyYear)
                .Where(x => x.ScheduleInfo.SemesterId == semester)
                .Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.ScheduleInfo.Courses.Any(y => y.Id == courseId))
                .Where(x => x.ScheduleInfo.Groups.Any(y => groupIds.Contains(y.Id)))
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForLecturer(
            int lecturerId,
            int studyYearId,
            int semester)
        {
            var result = GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYearId == studyYearId)
                .Where(x => x.ScheduleInfo.SemesterId == semester)
                .Where(x => x.ScheduleInfo.LecturerId.Equals(lecturerId))
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));

            return result;
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            int auditoriumId,
            int studyYearId,
            int semester)
        {
            var result = GetSchedules()
               .Where(x => x.ScheduleInfo.StudyYearId == studyYearId)
               .Where(x => x.ScheduleInfo.SemesterId == semester)
               .Where(x => x.AuditoriumId == auditoriumId)
               .ToList()
               .Select(x => new ScheduleDataTransfer(x));

            return result;
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            int auditoriumId,
            DateTime date)
        {
            var studyYear = date.Month > 6
                ? Database.StudyYears.FirstOrDefault(x => x.StartYear == date.Year)
                : Database.StudyYears.FirstOrDefault(x => x.EndYear == date.Year);

            if (studyYear == null)
                return Enumerable.Empty<ScheduleDataTransfer>();

            var result = GetSchedules()
               .Where(x => x.ScheduleInfo.StudyYearId == studyYear.Id)
               .Where(x => x.AuditoriumId == auditoriumId)
               .ToList()
               .Select(x => new ScheduleDataTransfer(x));

            return result;
        }

        public IEnumerable<ScheduleTypeDataTransfer> GetScheduleTypes()
        {
            var result = Database.ScheduleTypes
                .ToList()
                .Select(x => new ScheduleTypeDataTransfer(x));

            return result;
        }
        #endregion

        public int CountSchedulesForScheduleInfoes(int scheduleInfoId)
        {
            return Database.Schedules.Count(x => x.ScheduleInfo.Id == scheduleInfoId);
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(int scheduleInfoId)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.Id == scheduleInfoId)
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));
        }

        public ScheduleDataTransfer GetScheduleById(int id)
        {
            return new ScheduleDataTransfer(
                GetSchedules().FirstOrDefault(schedule => schedule.Id == id));

        }

        private IEnumerable<Schedule> GetSchedules()
        {
            var result = Database.Schedules
                .Include(x => x.ScheduleInfo)
                .Include(x => x.ScheduleInfo.Lecturer)
                .Include(x => x.ScheduleInfo.Tutorial)
                .Include(x => x.ScheduleInfo.TutorialType)
                .Include(x => x.ScheduleInfo.Courses)
                .Include(x => x.ScheduleInfo.Groups)
                .Include(x => x.ScheduleInfo.Faculties)
                .Include(x => x.Type)
                .Include(x => x.WeekType)
                .Include(x => x.Auditorium)
                .Include(x => x.Auditorium.Building)
                .Include(x => x.Auditorium.AuditoriumType)
                .Include(x => x.Time);
            return result;
        }

        public TutorialDataTransfer GetTutorialById(TutorialDataTransfer tutorialDataTransfer)
        {
            return new TutorialDataTransfer(
                Database.Tutorials.FirstOrDefault(x => x.Id == tutorialDataTransfer.Id));
        }

        public IEnumerable<TutorialDataTransfer> GetTutorialsForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
                .Where(x => x.Groups.Any(y => y.Id == groupDataTransfer.Id))
                .ToList()
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IEnumerable<TutorialDataTransfer> GetTutorialsForSpeciality(
            int specialityId,
            int courseId)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Courses.Any(y => y.Id == courseId))
                .Where(x => x.Specialities.Any(y => y.Id == specialityId))
                .ToList()
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IEnumerable<TutorialDataTransfer> GetTutorialsForFaculty(
            int facultyId,
            int courseId)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.Courses.Any(y => y.Id == courseId))
                .ToList()
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IEnumerable<SpecialityDataTransfer> GetSpecialities(int branchId)
        {
            return Database.Specialities
                .Where(x => x.BranchId == branchId)
                .ToList()
                .Select(x => new SpecialityDataTransfer(x));
        }

        public IEnumerable<SpecialityDataTransfer> GetSpecialitiesForFaculti(int facultyId)
        {
            return Database.Specialities
                .Where(x => x.Faculties.Any(y => y.Id == facultyId))
                .ToList()
                .Select(x => new SpecialityDataTransfer(x));
        }

        public IEnumerable<TimeDataTransfer> GetTimesByIds(IEnumerable<int> timeIds)
        {
            return Database.Times
                .Where(x => timeIds.Any(y => y == x.Id))
                .ToList()
                .Select(x => new TimeDataTransfer(x));
        }

        public IEnumerable<TimeDataTransfer> GetTimes(int buidlingId)
        {
            return Database.Times
                .Where(x => x.Buildings.Any(y => y.Id == buidlingId))
                .ToList()
                .Select(x => new TimeDataTransfer(x));
        }

        public IEnumerable<TutorialTypeDataTransfer> GetTutorialTypes()
        {
            return Database.TutorialTypes.ToList().Select(x => new TutorialTypeDataTransfer(x));
        }

        public TutorialTypeDataTransfer GetTutorialTypeById(TutorialTypeDataTransfer tutorialTypeDataTransfer)
        {
            return new TutorialTypeDataTransfer(
                Database.TutorialTypes.FirstOrDefault(x => x.Id == tutorialTypeDataTransfer.Id));
        }

        public IEnumerable<WeekTypeDataTransfer> GetWeekTypes()
        {
            return Database.WeekTypes.ToList().Select(x => new WeekTypeDataTransfer(x));
        }

        public IEnumerable<StudyYearDataTransfer> GetStudyYears()
        {
            return Database.StudyYears.ToList().Select(x => new StudyYearDataTransfer(x));
        }

        public StudyYearDataTransfer GetStudyYear(DateTime date)
        {
            var studyYear = date.Month > 6 
                ? Database.StudyYears.FirstOrDefault(x => x.StartYear == date.Year) 
                : Database.StudyYears.FirstOrDefault(x => x.EndYear == date.Year);

            return new StudyYearDataTransfer(studyYear);
        }

        public IEnumerable<StudyTypeDataTransfer> GetStudyTypes()
        {
            return Database
                .StudyTypes
                .ToList()
                .Select(x => new StudyTypeDataTransfer(x));
        }

        public ScheduleDataTransfer Plan(
            int auditoriumId,
            int dayOfWeek,
            int scheduleInfoId,
            int timeId, 
            int weekTypeId,
            int typeId)
        {
            var schedule = new Schedule
            {
                AuditoriumId = auditoriumId,
                DayOfWeek = dayOfWeek,
                ScheduleInfoId = scheduleInfoId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TimeId = timeId,
                WeekTypeId = weekTypeId,
                TypeId = typeId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var hasCollisions = CountScheduleCollisions(dayOfWeek, timeId, weekTypeId) > 0;
            if(hasCollisions)
                throw new ScheduleCollisionException();

            var scheduleInfo = Database.ScheduleInfoes.First(x => x.Id == scheduleInfoId);

            Database.Schedules.Add(schedule);
            scheduleInfo.IsPlanned = true;

            Database.SaveChanges();

            return new ScheduleDataTransfer(schedule);
        }

        public void Unplan(int scheduleId)
        {
            
        }
    }
}
