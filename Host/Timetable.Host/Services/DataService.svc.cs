using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;
using Timetable.Data.Models.Scheduler;
using Timetable.Host.Interfaces;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class DataService : BaseService, IDataService
    {
        public IEnumerable<TimetableEntityDataTransfer> GetTimetableEntities()
        {
            return Database.TimetableEntities
                .Where(x => x.IsActual)
                .ToList()
                .Select(x => new TimetableEntityDataTransfer(x));
        }

        public bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer)
        {
            var schedulesCount = 0;
            var schedules = Database.Schedules
                .Where(x => x.IsActual
                        && x.AuditoriumId == scheduleDataTransfer.Auditorium.Id
                        && x.PeriodId == scheduleDataTransfer.Time.Id
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

        public IEnumerable<BranchDataTransfer> GetBranches()
        {
            return Database.Branches
                .Where(x => x.IsActual)
                .ToList()
                .Select(x => new BranchDataTransfer(x));
        }

        public IEnumerable<AuditoriumDataTransfer> GetAuditoriums(Models.Scheduler.BuildingDataTransfer buildingDataTransfer, AuditoriumTypeDataTransfer auditoriumTypeDataTransfer)
        {
            if (auditoriumTypeDataTransfer != null)
            {
                // TODO: Need tutorial type reference
                return Database.Auditoriums
                    .Where(x => x.Building.Id.Equals(buildingDataTransfer.Id))
                    .Where(x => x.AuditoriumType.Id == auditoriumTypeDataTransfer.Id)
                    .Include(x => x.Building)
                    .ToList()
                    .Select(x => new AuditoriumDataTransfer(x));
            }
            else
            {
                return Database.Auditoriums
                    .Where(x => x.Building.Id.Equals(buildingDataTransfer.Id))
                    .Where(x => x.AuditoriumType == null)
                    .Include(x => x.Building)
                    .ToList()
                    .Select(x => new AuditoriumDataTransfer(x));
            }

        }

        public IEnumerable<AuditoriumDataTransfer> GetFreeAuditoriums(
            BuildingDataTransfer buildingDataTransfer,
            int dayOfWeek,
            WeekTypeDataTransfer weekTypeDataTransfer,
            TimeDataTransfer timeDataTransfer,
            TutorialTypeDataTransfer tutorialTypeDataTransfer,
            AuditoriumTypeDataTransfer auditoriumTypeDataTransfer,
            int capacity,
            DateTime startDate,
            DateTime endDate)
        {

            IEnumerable<AuditoriumDataTransfer> freeAuditoriums;
            IEnumerable<AuditoriumDataTransfer> scheduledAuditoriums;

            if (weekTypeDataTransfer.Id == 1)
            {
                scheduledAuditoriums = Database.Schedules
                     .Where(x => x.IsActual)
                     .Where(x => x.StartDate <= endDate && x.EndDate >= startDate)
                     .Where(x => x.Period.Id == timeDataTransfer.Id)
                     .Where(x => x.DayOfWeek == dayOfWeek)
                     .Where(x => (x.WeekType.Id == weekTypeDataTransfer.Id || x.WeekType.Id == 2 || x.WeekType.Id == 3))
                     .ToList()
                     .Select(x => new AuditoriumDataTransfer(x.Auditorium));
            }
            else
            {
                scheduledAuditoriums = Database.Schedules
                     .Where(x => x.IsActual)
                     .Where(x => x.StartDate <= endDate && x.EndDate >= startDate)
                     .Where(x => x.Period.Id == timeDataTransfer.Id)
                     .Where(x => x.DayOfWeek == dayOfWeek)
                     .Where(x => (x.WeekType.Id == weekTypeDataTransfer.Id || x.WeekType.Id == 1))
                     .ToList()
                     .Select(x => new AuditoriumDataTransfer(x.Auditorium));
            }



            if (tutorialTypeDataTransfer == null)
            {
                freeAuditoriums = Database.Auditoriums
                    .Where(x => x.Building.Id == buildingDataTransfer.Id)
                    .Where(x => x.Capacity >= capacity)
                    .Where(x => x.AuditoriumType.Id == auditoriumTypeDataTransfer.Id)
                    .Where(x => !scheduledAuditoriums.Any(y => y.Id == x.Id))
                    .ToList()
                    .Select(x => new AuditoriumDataTransfer(x));
            }
            else
            {
                freeAuditoriums = Database.Auditoriums
                    .Where(x => x.Building.Id == buildingDataTransfer.Id)
                    .Where(x => x.Capacity >= capacity)
                    .Where(x => x.AuditoriumType.Id == auditoriumTypeDataTransfer.Id)
                    .Where(x => !scheduledAuditoriums.Any(y => y.Id == x.Id))
                    .Where(x => x.TutorialApplicabilities.Any(y => y.Id == tutorialTypeDataTransfer.Id))
                    .ToList()
                    .Select(x => new AuditoriumDataTransfer(x));

            }

            return freeAuditoriums;
        }

        public IEnumerable<BuildingDataTransfer> GetBuildings()
        {
            var result = Database.Buildings.ToList().Select(x => new BuildingDataTransfer(x));
            return result;
        }

        public IEnumerable<CourseDataTransfer> GetCources()
        {
            return Database.Courses.Where(x => x.IsActual).ToList().Select(x => new CourseDataTransfer(x));
        }

        public IEnumerable<DepartmentDataTransfer> GetDeparmtents()
        {
            return Database.Departments.ToList().Select(x => new DepartmentDataTransfer(x));
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

        public IEnumerable<GroupDataTransfer> GetGroupsForFaculty(int facultyId, int[] courseIds)
        {
            var result = Database.Groups
                .Where(x => x.IsActual)
                .Where(x => courseIds.Contains(x.CourseId))
                .Where(x => x.FacultyId == facultyId)
                .ToList()
                .Select(x => new GroupDataTransfer(x));

            return result;
        }

        public IEnumerable<GroupDataTransfer> GetGroupsForSpeciality(int specialityId, int[] courseIds)
        {
            var result = Database.Groups
                .Where(x => x.IsActual)
                .Where(x => courseIds.Contains(x.CourseId))
                .Where(x => x.Speciality.Id == specialityId)
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

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester)
        {
            return GetScheduleInfoes()
                .Where(x => x.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.Semester == semester)
                .Where(x => x.Faculties.Any(f => f.Id.Equals(facultyDataTransfer.Id)))
                .Where(x => x.Courses.Any(c => c.Id.Equals(courseDataTransfer.Id)))
                .ToList()
                .Select(x => new ScheduleInfoDataTransfer(x));
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester)
        {
            return GetScheduleInfoesForCourse(facultyDataTransfer, courseDataTransfer, studyYearDataTransfer, semester)
                .Where(x => x.Specialities.Any(y => y.Id.Equals(specialityDataTransfer.Id)));
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForFaculty(
            int facultyId,
            int courseId,
            int studyYear,
            int semester)
        {
            return Database.ScheduleInfoes
                .Where(x => x.StudyYear.Id == studyYear)
                .Where(x => x.Semester == semester)
                .Where(x => x.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.Courses.Any(y => y.Id == courseId))
                .Include(x => x.Lecturer)
                .Include(x => x.Tutorial)
                .Include(x => x.TutorialType)
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
                .Where(x => x.Semester == semester)
                .Where(x => x.Groups.Any(y => groupIds.Contains(y.Id)))
                .Include(x => x.Lecturer)
                .Include(x => x.Tutorial)
                .Include(x => x.TutorialType)
                .ToList()
                .Select(x => new ScheduleInfoDataTransfer(x));
        }

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForDepartment(
            DepartmentDataTransfer departmentDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester)
        {
            return GetScheduleInfoes()
                .Where(x => x.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.Semester == semester)
                .Where(x => x.Department.Id.Equals(departmentDataTransfer.Id))
                .ToList()
                .Select(x => new ScheduleInfoDataTransfer(x));
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

        public IEnumerable<AuditoriumTypeDataTransfer> GetAuditoriumTypes()
        {
            return Database.AuditoriumTypes.ToList().Select(x => new AuditoriumTypeDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForAll(
            LecturerDataTransfer lecturerDataTransfer,
            AuditoriumDataTransfer auditoriumDataTransfer,
            IEnumerable<GroupDataTransfer> groups,
            WeekTypeDataTransfer weekTypeDataTransfer,
            string subGroup,
            DateTime startDate,
            DateTime endDate)
        {
            var result = GetSchedules();
            if (lecturerDataTransfer != null)
                result = result.Where(x => x.ScheduleInfo.Lecturer.Id == lecturerDataTransfer.Id);
            if (auditoriumDataTransfer != null)
                result = result.Where(x => x.Auditorium.Id == auditoriumDataTransfer.Id);

            foreach (var group in groups)
                result = result.Where(x => x.ScheduleInfo.Groups.Any(y => y.Id == group.Id));

            if (subGroup != null)
                result = result.Where(x => x.SubGroup == null || x.SubGroup == subGroup);

            if (startDate != null)
                result = result.Where(x => x.EndDate >= startDate);

            if (endDate != null)
                result = result.Where(x => x.StartDate <= endDate);

            if (weekTypeDataTransfer != null)
                if (weekTypeDataTransfer.Id == 2)
                    result = result.Where(x => x.WeekType.Id != 3);
                else if (weekTypeDataTransfer.Id == 3)
                    result = result.Where(x => x.WeekType.Id != 2);

            //TODO: order by priority
            var query = result.GroupBy(x => new { x.DayOfWeek, x.Period.Id });

            //TODO: improuve speed
            var answer = new List<ScheduleDataTransfer>();
            foreach (var q in query)
                answer.Add(new ScheduleDataTransfer(q.OrderBy(x => x.CreatedDate).First()));

            return answer.AsQueryable();
        }


        public IEnumerable<ScheduleDataTransfer> GetSchedulesForDayTimeDate(
            int? dayOfWeek,
            TimeDataTransfer period,
            WeekTypeDataTransfer weekTypeDataTransfer,
            LecturerDataTransfer lecturerDataTransfer,
            AuditoriumDataTransfer auditoriumDataTransfer,
            IEnumerable<GroupDataTransfer> groups,
            string subGroup,
            DateTime startDate,
            DateTime endDate)
        {
            var result = GetSchedules().Where(x => x.IsActual);

            if (dayOfWeek != null)
                result = result.Where(x => x.DayOfWeek == dayOfWeek);

            if (period != null)
                result = result.Where(x => x.Period.Id == period.Id);

            if (lecturerDataTransfer != null)
                result = result.Where(x => x.ScheduleInfo.Lecturer.Id == lecturerDataTransfer.Id);
            if (auditoriumDataTransfer != null)
                result = result.Where(x => x.Auditorium.Id == auditoriumDataTransfer.Id);
            foreach (var group in groups)
                result = result.Where(x => x.ScheduleInfo.Groups.Any(y => y.Id == group.Id));
            if (subGroup != null)
                result = result.Where(x => x.SubGroup == null || x.SubGroup == subGroup);

            if (startDate != null)
                result = result.Where(x => x.EndDate >= startDate);
            if (endDate != null)
                result = result.Where(x => x.StartDate <= endDate);

            //TODO: order by priority
            return result.OrderBy(x => x.CreatedDate).ToList().Select(x => new ScheduleDataTransfer(x));
        }

        public int CountScheduleCollisions(
            int day,
            TimeDataTransfer timeDataTransfer,
            WeekTypeDataTransfer weekTypeDataTransfer)
        {
            return Database.Schedules.Count(x => x.Period.Id.Equals(timeDataTransfer.Id) && x.DayOfWeek.Equals(day) &&
                (x.WeekType.Id == 1 || x.WeekType.Id == weekTypeDataTransfer.Id));
        }

        #region schedule
        public IEnumerable<ScheduleDataTransfer> GetSchedulesByDayTime(int day, TimeDataTransfer timeDataTransfer)
        {
            return GetSchedules()
                .Where(x => (x.DayOfWeek == day && x.Period.Id == timeDataTransfer.Id))
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForFaculty(
            int facultyId,
            int courseId,
            int studyYearId,
            int semester)
        {
            var result = GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearId)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id == facultyId))
                .Where(x => x.ScheduleInfo.Courses.Any(y => y.Id == courseId));

            return result.ToList().Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForSpeciality(
            int specialityId,
            int courseId,
            int studyYearId,
            int semester)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearId)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Courses.Any(y => y.Id == courseId) && x.ScheduleInfo.Specialities.Any(y => y.Id == specialityId))
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForGroups(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYear)
                .Where(x => x.ScheduleInfo.Semester == semester)
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
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearId)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Lecturer.Id.Equals(lecturerId))
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
               .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearId)
               .Where(x => x.ScheduleInfo.Semester == semester)
               .Where(x => x.Auditorium.Id == auditoriumId)
               .ToList()
               .Select(x => new ScheduleDataTransfer(x));

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
            return Database.Schedules
                .Include(x => x.ScheduleInfo)
                .Include(x => x.ScheduleInfo.Lecturer)
                .Include(x => x.ScheduleInfo.Tutorial)
                .Include(x => x.ScheduleInfo.TutorialType)
                .Include(x => x.ScheduleInfo.Courses)
                .Include(x => x.ScheduleInfo.Groups)
                .Include(x => x.ScheduleInfo.Faculties)
                .Include(x => x.WeekType)
                .Include(x => x.Auditorium)
                .Include(x => x.Period);
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
    }
}
