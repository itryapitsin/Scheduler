using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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

        public SemesterDataTransfer GetSemesterForTime(DateTime ?date)
        {
            if (date.HasValue)
            {
                if (date.Value.Month > 6)
                    return Database.Semesters.ToList()
                        .Select(x => new SemesterDataTransfer(x))
                        .FirstOrDefault(x => x.Name == "Первый семестр");
                return Database.Semesters.ToList()
                        .Select(x => new SemesterDataTransfer(x))
                        .FirstOrDefault(x => x.Name == "Второй семестр");
            }

            return null;
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
            int? weekTypeId,
            int pair,
            int? scheduleId = null)
        {
            var time = Database.Times.FirstOrDefault(x => x.Position == pair && x.Buildings.Any(y => y.Id == buildingId));

            var weekType = Database.WeekTypes
                .Where(x => x.IsActual)
                .FirstOrDefault(x => x.Name == "Л");


            var scheduledAuditoriums = weekTypeId.HasValue ?

                scheduleId.HasValue ?
                Database.Schedules
                .Where(x => x.Id != scheduleId.Value)
                .Where(x => x.IsActual)
                .Where(x => x.Time.Id == time.Id)
                .Where(x => x.DayOfWeek == dayOfWeek)
                .Where(x => (x.WeekType.Id == weekTypeId.Value || x.WeekType.Id == weekType.Id))
                .Select(x => x.Auditorium) :
                Database.Schedules
                .Where(x => x.IsActual)
                .Where(x => x.Time.Id == time.Id)
                .Where(x => x.DayOfWeek == dayOfWeek)
                .Where(x => (x.WeekType.Id == weekTypeId.Value || x.WeekType.Id == weekType.Id))
                .Select(x => x.Auditorium)
                :
                scheduleId.HasValue ?
                Database.Schedules
                .Where(x => x.Id != scheduleId.Value)
                .Where(x => x.IsActual)
                .Where(x => x.Time.Id == time.Id)
                .Where(x => x.DayOfWeek == dayOfWeek)
                .Select(x => x.Auditorium) :
                Database.Schedules
                .Where(x => x.IsActual)
                .Where(x => x.Time.Id == time.Id)
                .Where(x => x.DayOfWeek == dayOfWeek)
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

        public FacultyDataTransfer GetFacultyById(int facultyId)
        {
            return Database.Faculties
                .Where(x => x.Id == facultyId)
                .ToList()
                .Select(x => new FacultyDataTransfer(x)).FirstOrDefault();
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

        //Get actual and not actual groups
        public IEnumerable<GroupDataTransfer> GetAllGroupsForFaculty(int facultyId, int courseId, int studyTypeId)
        {
            var result = Database.Groups
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


        //depricated
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

        //new
        public LecturerDataTransfer GetLecturerBySearchString(string searchString)
        {
            return GetLecturersBySearchString(searchString).FirstOrDefault();
        }

        public IEnumerable<LecturerDataTransfer> GetLecturersBySearchString(string searchString)
        {
            var tokens = searchString.Replace(".", "")
                                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var lastName = "";
            var firstName = "";
            var middleName = "";

            if (tokens.Length == 1)
            {
                lastName = tokens[0];
                return Database.Lecturers.Where(
                              x => x.Lastname == lastName)
                             .ToList()
                             .Select(x => new LecturerDataTransfer(x));
                             

            }
            if (tokens.Length == 2)
            {
                lastName = tokens[0];
                firstName = tokens[1];
                return Database.Lecturers.Where(
                              x => x.Lastname == lastName &&
                              x.Firstname.StartsWith(firstName))
                             .ToList()
                             .Select(x => new LecturerDataTransfer(x));
                           
            }

            lastName = tokens[0];
            firstName = tokens[1];
            middleName = tokens[2];
            return Database.Lecturers.Where(
                          x => x.Lastname == lastName &&
                          x.Firstname.StartsWith(firstName) &&
                          x.Middlename.StartsWith(middleName))
                         .ToList()
                         .Select(x => new LecturerDataTransfer(x));
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
                .Include(x => x.Groups)
                .Include(x => x.Department)
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
            int weekTypeId,
            int auditoriumId,
            int scheduleInfoId,
            int? scheduleId = null)
        {
            var weekType = Database.WeekTypes
                .Where(x => x.IsActual)
                .FirstOrDefault(x => x.Name == "Л");

            var scheduleInfo = Database.ScheduleInfoes
                .Where(x => x.Id == scheduleInfoId)
                .Include(y => y.Groups)
                .Include(y => y.Lecturer)
                .FirstOrDefault();

            var groupIds = scheduleInfo.Groups.Select(x => x.Id).AsEnumerable();
          

            var result = Database.Schedules
                .Where(x => x.ScheduleInfo.Lecturer.Id == scheduleInfo.Lecturer.Id ||
                            x.Auditorium.Id == auditoriumId ||
                            (x.ScheduleInfo.Groups.Select(y => y.Id).Intersect(groupIds).Count() > 0))
                .Where(x => x.IsActual)
                .Where(x => x.Time.Id == timeId)
                .Where(x => x.DayOfWeek == day)
                .Where(x => ((x.WeekType.Id == weekTypeId) || (x.WeekType.Id == weekType.Id)));

          

            if (scheduleId.HasValue)
                return result.Where(x => x.Id != scheduleId.Value).Count();
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

            return result.ToList()
                .GroupBy(x => new {x.DayOfWeek, x.WeekType, x.Time, x.Auditorium}).Select(x => x.FirstOrDefault())
                .Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedules(
            int[] auditoriumIds,
            int studyYear,
            int semester,
            int timeId,
            int dayOfWeek)
        {
            if (auditoriumIds.Count() == 0)
                return Enumerable.Empty<ScheduleDataTransfer>();

            var result = GetSchedules()
                .Where(x => auditoriumIds.Any(y => y == x.AuditoriumId))
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYear)
                .Where(x => x.ScheduleInfo.Semester.Id == semester)
                .Where(x => x.Time.Id == timeId && x.DayOfWeek == dayOfWeek);

            return result.ToList()
                   .GroupBy(x => new {x.WeekType, x.Auditorium }).Select(x => x.FirstOrDefault())
                   .Select(x => new ScheduleDataTransfer(x));
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

            return result.ToList()
                .GroupBy(x => new { x.DayOfWeek, x.WeekType, x.Time, x.Auditorium }).Select(x => x.FirstOrDefault())
                .Select(x => new ScheduleDataTransfer(x));
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

            return result.ToList()
                .GroupBy(x => new { x.DayOfWeek, x.WeekType, x.Time, x.Auditorium })
                .Select(x => x.FirstOrDefault())
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
                .Where(x => x.ScheduleInfo.StudyYearId == studyYear)
                .Where(x => x.ScheduleInfo.SemesterId == semester)
                //.Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id == facultyId))
                //.Where(x => x.ScheduleInfo.Courses.Any(y => y.Id == courseId))
                .Where(x => x.ScheduleInfo.Groups.Any(y => groupIds.Contains(y.Id)))
                .ToList()
                .GroupBy(x => new { x.DayOfWeek, x.WeekType, x.Time, x.Auditorium })
                .Select(x => x.FirstOrDefault()) 
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
                .GroupBy(x => new { x.DayOfWeek, x.WeekType, x.Time })
                .Select(x => x.FirstOrDefault())
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
               .GroupBy(x => new {x.DayOfWeek, x.WeekType, x.Time })
               .Select(x => x.FirstOrDefault())
               .Select(x => new ScheduleDataTransfer(x));

            return result;
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            int auditoriumId,
            DateTime date)
        {
            var studyYear = date.Month > 6
                ? Database.StudyYears.FirstOrDefault(x => x.StartYear == date.Year)
                : Database.StudyYears.FirstOrDefault(x => x.StartYear+x.Length == date.Year);

            if (studyYear == null)
                return Enumerable.Empty<ScheduleDataTransfer>();

            var semester = GetSemesterForTime(date);

            var result = GetSchedules()
               .Where(x => x.ScheduleInfo.StudyYearId == studyYear.Id)
               .Where(x => x.AuditoriumId == auditoriumId)
               .Where(x => x.ScheduleInfo.Semester.Id == semester.Id)
               .ToList()
               .GroupBy(x => new {x.DayOfWeek, x.WeekType, x.Time })
               .Select(x => x.FirstOrDefault())
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
            //TODO: Исправить подсчет в соответствии с GroupBy
            return Database.Schedules.Count(x => x.ScheduleInfo.Id == scheduleInfoId);
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(int scheduleInfoId)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.Id == scheduleInfoId)
                .ToList()
                .GroupBy(x => new {x.DayOfWeek, x.WeekType, x.Time, x.Auditorium }).Select(x => x.FirstOrDefault())
                .Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForSchedule(int scheduleId)
        {
            var schedule = Database.Schedules.Where(x => x.Id == scheduleId)
                .Include(x => x.ScheduleInfo)
                .Include(x => x.ScheduleInfo.Lecturer)
                .Include(x => x.ScheduleInfo.Groups)
                .Include(x => x.Auditorium)
                .FirstOrDefault();

            var groupIds = schedule.ScheduleInfo.Groups.Select(x => x.Id).AsEnumerable();

            return GetSchedules()
                .Where(x => x.ScheduleInfo.Lecturer.Id == schedule.ScheduleInfo.Lecturer.Id ||
                            x.Auditorium.Id == schedule.Auditorium.Id ||
                            (x.ScheduleInfo.Groups.Select(y => y.Id).Intersect(groupIds).Count() > 0))
                .ToList()
                .GroupBy(x => new { x.DayOfWeek, x.WeekType, x.Time, x.Auditorium }).Select(x => x.FirstOrDefault())
                .Select(x => new ScheduleDataTransfer(x, (new Func<IEnumerable<ScheduleState>> (() =>
                {
                    var states = new List<ScheduleState>();
                    if (x.ScheduleInfo.Lecturer.Id == schedule.ScheduleInfo.Lecturer.Id)
                        states.Add(ScheduleState.RelatedWithLecturer);

                    if ((x.ScheduleInfo.Groups.Select(y => y.Id).Intersect(groupIds).Count() > 0))
                        states.Add(ScheduleState.RelatedWithThread);

                    if (x.Auditorium.Id == schedule.Auditorium.Id)
                        states.Add(ScheduleState.RelatedWithAuditorium);

                    return states;
                })).Invoke()));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfo(int scheduleInfoId)
        {
            var scheduleInfo = Database.ScheduleInfoes.Where(x => x.Id == scheduleInfoId)
                .Include(x => x.Lecturer)
                .Include(x => x.Groups)
                .FirstOrDefault();

            var groupIds = scheduleInfo.Groups.Select(x => x.Id).AsEnumerable();

            return GetSchedules()
                .Where(x => x.ScheduleInfo.Lecturer.Id == scheduleInfo.Lecturer.Id ||
                            (x.ScheduleInfo.Groups.Select(y => y.Id).Intersect(groupIds).Count() > 0))
                .ToList()
                .GroupBy(x => new { x.DayOfWeek, x.WeekType, x.Time, x.Auditorium }).Select(x => x.FirstOrDefault())
                .Select(x => new ScheduleDataTransfer(x, (new Func<IEnumerable<ScheduleState>> (() => {

                    var states = new List<ScheduleState>();

                    if (x.ScheduleInfo.Lecturer.Id == scheduleInfo.Lecturer.Id)
                        states.Add(ScheduleState.RelatedWithLecturer);

                    if ((x.ScheduleInfo.Groups.Select(y => y.Id).Intersect(groupIds).Count() > 0))
                        states.Add(ScheduleState.RelatedWithThread);

                    return states; 
                })).Invoke()));
                /*.Select(x => new ScheduleDataTransfer(x,
                            x.ScheduleInfo.Lecturer.Id == scheduleInfo.Lecturer.Id ?
                            ScheduleState.RelatedWithLecturer :
                            ScheduleState.RelatedWithThread));*/
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
                .Include(x => x.ScheduleInfo.StudyYear)
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


        public IEnumerable<TutorialDataTransfer> GetTutorialsBySearchString(string searchString)
        {
            return Database.Tutorials.Where(x => x.Name.Contains(searchString) || searchString.Contains(x.Name) ||
                x.ShortName.Contains(searchString) || searchString.Contains(x.ShortName)).ToList()
                .Select(x => new TutorialDataTransfer(x));
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

        public IEnumerable<DepartmentDataTransfer> GetDepartments()
        {
            return Database.Departments.ToList().Select(x => new DepartmentDataTransfer(x));
        }

        public IEnumerable<PositionDataTransfer> GetPositions()
        {
            return Database.Positions.ToList().Select(x => new PositionDataTransfer(x));
        }

        public IEnumerable<StudyYearDataTransfer> GetStudyYears()
        {
            return Database.StudyYears.ToList().Select(x => new StudyYearDataTransfer(x));
        }

        public StudyYearDataTransfer GetStudyYear(DateTime date)
        {
            var studyYear = date.Month > 6 
                ? Database.StudyYears.FirstOrDefault(x => x.StartYear == date.Year) 
                : Database.StudyYears.FirstOrDefault(x => x.StartYear+x.Length == date.Year);

            return new StudyYearDataTransfer(studyYear);
        }

        public IEnumerable<StudyTypeDataTransfer> GetStudyTypes()
        {
            return Database
                .StudyTypes
                .ToList()
                .Select(x => new StudyTypeDataTransfer(x));
        }


        public ScheduleDataTransfer PlanEdit(
            int auditoriumId,
            int dayOfWeek,
            int scheduleId,
            int timeId,
            int weekTypeId,
            int typeId,
            string subGroup
            )
        {
            if (auditoriumId == 0 || dayOfWeek == 0 || scheduleId == 0 || timeId == 0 || weekTypeId == 0 || typeId == 0)
                throw new ScheduleNoDataException();

            var schedule = Database.Schedules.Where(x => x.Id == scheduleId).Include(x => x.ScheduleInfo).FirstOrDefault();

            var hasCollisions = CountScheduleCollisions(dayOfWeek, timeId, auditoriumId, weekTypeId, schedule.ScheduleInfo.Id, scheduleId) > 0;
            if (hasCollisions)
                throw new ScheduleCollisionException();

            if (schedule.AuditoriumId != auditoriumId)
                schedule.Auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId).FirstOrDefault();

            if (schedule.TimeId != timeId)
                 schedule.Time = Database.Times.Where(x => x.Id == timeId).FirstOrDefault();

            if(schedule.WeekTypeId != weekTypeId)
                schedule.WeekType = Database.WeekTypes.Where(x => x.Id == weekTypeId).FirstOrDefault();

            if (schedule.TypeId != typeId)
                schedule.Type = Database.ScheduleTypes.Where(x => x.Id == typeId).FirstOrDefault();

            schedule.DayOfWeek = dayOfWeek;
            schedule.SubGroup = subGroup;
            

            Database.Update(schedule);

            return GetScheduleById(scheduleId);
        }

        public ScheduleDataTransfer Plan(
            int auditoriumId,
            int dayOfWeek,
            int scheduleInfoId,
            int timeId, 
            int weekTypeId,
            int typeId,
            string subGroup)
        {
            if (auditoriumId == 0 || dayOfWeek == 0 || scheduleInfoId == 0 || timeId == 0 || weekTypeId == 0 || typeId == 0)
                throw new ScheduleNoDataException();

           

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
                EndDate = DateTime.Now,
                SubGroup = subGroup,
                IsActual = true,
            };



            var hasCollisions = CountScheduleCollisions(dayOfWeek, timeId, auditoriumId, weekTypeId, scheduleInfoId) > 0;
            if(hasCollisions)
                throw new ScheduleCollisionException();

            var scheduleInfo = Database.ScheduleInfoes.First(x => x.Id == scheduleInfoId);

            scheduleInfo.IsPlanned = true;

            Database.Schedules.Add(schedule);
            

            Database.SaveChanges();

            var newschedule = Database.Schedules.Where(x=> x.ScheduleInfo.Id == scheduleInfoId
                                                        && x.Auditorium.Id == auditoriumId &&
                                                        x.WeekType.Id == weekTypeId &&
                                                        x.Time.Id == timeId &&
                                                        x.Type.Id == typeId &&
                                                        x.DayOfWeek == dayOfWeek).FirstOrDefault();
            var test = GetScheduleById(newschedule.Id);
            return GetScheduleById(newschedule.Id);
        }

        public void Unplan(int scheduleId)
        {
            var schedule = Database.Schedules.Where(x => x.Id == scheduleId).FirstOrDefault();
            Database.Schedules.Remove(schedule);
            Database.SaveChanges();
        }


        public void EditAuditorium(
            string number,
            string name,
            string info,
            int? capacity,
            int buildingId,
            int auditoriumTypeId,
            int auditoriumId
            )
        {
            var auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId).FirstOrDefault();
            var building = Database.Buildings.Where(x => x.Id == buildingId).FirstOrDefault();
            var auditoriumType = Database.AuditoriumTypes.Where(x => x.Id == auditoriumTypeId).FirstOrDefault();
            auditorium.Number = number;
            auditorium.Name = name;
            auditorium.Info = info;
            auditorium.Capacity = capacity;
            auditorium.Building = building;
            auditorium.AuditoriumType = auditoriumType;
            auditorium.IsActual = true;
            auditorium.CreatedDate = DateTime.Now;
            auditorium.UpdatedDate = DateTime.Now;

            Database.Update(auditorium);
        }

        public void CreateAuditorium(
            string number,
            string name,
            string info,
            int? capacity,
            int buildingId,
            int auditoriumTypeId
            )
        {
            var building = Database.Buildings.Where(x => x.Id == buildingId).FirstOrDefault();
            var auditoriumType = Database.AuditoriumTypes.Where(x => x.Id == auditoriumTypeId).FirstOrDefault();
            Database.Auditoriums.Add(
                    

                    new Auditorium
                    {
                        Number = number,
                        Name = name,
                        Info = info,
                        Capacity = capacity,
                        Building = building,
                        AuditoriumType = auditoriumType,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteAuditorium(int auditoriumId)
        {
            var auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId).FirstOrDefault();
            Database.Auditoriums.Remove(auditorium);
            Database.SaveChanges();
        }


        public void EditAuditoriumType(
           string name,
           string pattern,
           int auditoriumTypeId
           )
        {
            var auditoriumType = Database.AuditoriumTypes.Where(x => x.Id == auditoriumTypeId).FirstOrDefault();

            auditoriumType.Name = name;
            auditoriumType.Pattern = pattern;
            auditoriumType.Training = true;
            auditoriumType.IsActual = true;
            auditoriumType.CreatedDate = DateTime.Now;
            auditoriumType.UpdatedDate = DateTime.Now;

            Database.Update(auditoriumType);
        }

        public void CreateAuditoriumType(
               string name,
               string pattern
            )
        {
          
            Database.AuditoriumTypes.Add(
                    new AuditoriumType
                    {
                        Name = name,
                        Pattern = pattern,
                        Training = true,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteAuditoriumType(int auditoriumTypeId)
        {
            var auditoriumType = Database.AuditoriumTypes.Where(x => x.Id == auditoriumTypeId).FirstOrDefault();
            Database.AuditoriumTypes.Remove(auditoriumType);
            Database.SaveChanges();
        }


        public void EditWeekType(
           string name,
           int weekTypeId
           )
        {
            var weekType = Database.WeekTypes.Where(x => x.Id == weekTypeId).FirstOrDefault();

            weekType.Name = name;
            weekType.IsActual = true;
            weekType.CreatedDate = DateTime.Now;
            weekType.UpdatedDate = DateTime.Now;

            Database.Update(weekType);
        }

        public void CreateWeekType(
               string name
            )
        {

            Database.WeekTypes.Add(
                    new WeekType
                    {
                        Name = name,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteWeekType(int weekTypeId)
        {
            var weekType = Database.WeekTypes.Where(x => x.Id == weekTypeId).FirstOrDefault();
            Database.WeekTypes.Remove(weekType);
            Database.SaveChanges();
        }

        public void EditStudyYear(
           int startYear,
           int length,
            int studyYearId
           )
        {
            var studyYear = Database.StudyYears.Where(x => x.Id == studyYearId).FirstOrDefault();

            studyYear.StartYear = startYear;
            studyYear.Length = length;
            studyYear.IsActual = true;
            studyYear.CreatedDate = DateTime.Now;
            studyYear.UpdatedDate = DateTime.Now;

            Database.Update(studyYear);
        }

        public void CreateStudyYear(
                int startYear,
                int length
            )
        {

            Database.StudyYears.Add(
                    new StudyYear
                    {
                        StartYear = startYear,
                        Length = length,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteStudyYear(int studyYearId)
        {
            var studyYear = Database.StudyYears.Where(x => x.Id == studyYearId).FirstOrDefault();
            Database.StudyYears.Remove(studyYear);
            Database.SaveChanges();
        }

        public void EditSemester(
           string name,
           int semesterId
           )
        {
            var semester = Database.Semesters.Where(x => x.Id == semesterId).FirstOrDefault();

            semester.Name = name;
            semester.IsActual = true;
            semester.CreatedDate = DateTime.Now;
            semester.UpdatedDate = DateTime.Now;

            Database.Update(semester);
        }

        public void CreateSemester(
               string name
            )
        {

            Database.Semesters.Add(
                    new Semester
                    {
                        Name = name,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteSemester(int semesterId)
        {
            var semester = Database.Semesters.Where(x => x.Id == semesterId).FirstOrDefault();
            Database.Semesters.Remove(semester);
            Database.SaveChanges();
        }

        public void EditScheduleType(
           string name,
           int scheduleTypeId
           )
        {
            var scheduleType = Database.ScheduleTypes.Where(x => x.Id == scheduleTypeId).FirstOrDefault();

            scheduleType.Name = name;
            scheduleType.IsActive = true;
            scheduleType.IsActual = true;
            scheduleType.CreatedDate = DateTime.Now;
            scheduleType.UpdatedDate = DateTime.Now;

            Database.Update(scheduleType);
        }

        public void CreateScheduleType(
               string name
            )
        {

            Database.ScheduleTypes.Add(
                    new ScheduleType
                    {
                        Name = name,
                        IsActive = true,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteScheduleType(int scheduleTypeId)
        {
            var scheduleType = Database.ScheduleTypes.Where(x => x.Id == scheduleTypeId).FirstOrDefault();
            Database.ScheduleTypes.Remove(scheduleType);
            Database.SaveChanges();
        }

        public void EditTutorialType(
           string name,
           int tutorialTypeId
           )
        {
            var tutorialType = Database.TutorialTypes.Where(x => x.Id == tutorialTypeId).FirstOrDefault();

            tutorialType.Name = name;
            tutorialType.IsActual = true;
            tutorialType.CreatedDate = DateTime.Now;
            tutorialType.UpdatedDate = DateTime.Now;

            Database.Update(tutorialType);
        }

        public void CreateTutorialType(
               string name
            )
        {

            Database.TutorialTypes.Add(
                    new TutorialType
                    {
                        Name = name,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteTutorialType(int tutorialTypeId)
        {
            var tutorialType = Database.TutorialTypes.Where(x => x.Id == tutorialTypeId).FirstOrDefault();
            Database.TutorialTypes.Remove(tutorialType);
            Database.SaveChanges();
        }

        public void EditStudyType(
           string name,
           int studyTypeId
           )
        {
            var studyType = Database.StudyTypes.Where(x => x.Id == studyTypeId).FirstOrDefault();

            studyType.Name = name;
            studyType.IsActual = true;
            studyType.CreatedDate = DateTime.Now;
            studyType.UpdatedDate = DateTime.Now;

            Database.Update(studyType);
        }

        public void CreateStudyType(
               string name
            )
        {

            Database.StudyTypes.Add(
                    new StudyType
                    {
                        Name = name,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteStudyType(int studyTypeId)
        {
            var studyType = Database.StudyTypes.Where(x => x.Id == studyTypeId).FirstOrDefault();
            Database.StudyTypes.Remove(studyType);
            Database.SaveChanges();
        }

        public void EditCourse(
           string name,
           int [] branchIds,
           int courseId
           )
        {
            var course = Database.Courses.Where(x => x.Id == courseId).FirstOrDefault();
            var branches = Database.Branches.Where(x => branchIds.Any(y => y == x.Id)).ToList();

            course.Branches = branches;

            course.Name = name;
            course.IsActual = true;
            course.CreatedDate = DateTime.Now;
            course.UpdatedDate = DateTime.Now;

            Database.Update(course);
        }

        public void CreateCourse(
               string name,
               int[] branchIds
            )
        {
            var branches = Database.Branches.Where(x => branchIds.Any(y => y == x.Id)).ToList();


            Database.Courses.Add(
                    new Course
                    {
                        Name = name,
                        Branches = branches,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            var course = Database.Courses.Where(x => x.Id == courseId).FirstOrDefault();
            Database.Courses.Remove(course);
            Database.SaveChanges();
        }

        public void EditTime(
           string start,
           string end,
           int position,
           int[] buildingIds,
           int timeId
           )
        {
           
            var time = Database.Times.Where(x => x.Id == timeId).FirstOrDefault();
            var buildings = Database.Buildings.Where(x => buildingIds.Any(y => y == x.Id)).ToList();


            time.Buildings = buildings;
            time.Position = position;
            time.Start = TimeSpan.ParseExact(start, "mm'.'ss", null);
            time.End = TimeSpan.ParseExact(end, "mm'.'ss", null);
            time.IsActual = true;
            time.CreatedDate = DateTime.Now;
            time.UpdatedDate = DateTime.Now;

            Database.Update(time);
        }

        public void CreateTime(
               string start,
               string end,
               int position,
               int[] buildingIds
            )
        {
            var buildings = Database.Buildings.Where(x => buildingIds.Any(y => y == x.Id)).ToList();


            Database.Times.Add(
                    new Time
                    {
                        Start = TimeSpan.ParseExact(start, "mm'.'ss", null),
                        End = TimeSpan.ParseExact(end, "mm'.'ss", null),
                        Position = position,
                        Buildings = buildings,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteTime(int timeId)
        {
            var time = Database.Times.Where(x => x.Id == timeId).FirstOrDefault();
            Database.Times.Remove(time);
            Database.SaveChanges();
        }


        /*
        void Edit(TEntity oldEntity) where TEntity : BaseIIASEntity
        {
            var oldEntity Database.Set<TEntity>().Where(x => x.Id == oldEntity.Id).FirstOfDefault();
            oldEntity = newEntity;

            Database.Update<TEntity>(entity);
         
            Database.SaveChanges();
        }

        void Create<TEntity>(TEntity entity) where TEntity : BaseIIASEntity
        {
            Database.Add<TEntity>(entity);
         
            Database.SaveChanges();
        }*/


        public void EditLecturer(
           string firstName,
           string middleName,
           string lastName,
           string contacts,
           int[] positionIds,
           int[] departmentIds,
           int lecturerId
           )
        {

            var lecturer = Database.Lecturers.Where(x => x.Id == lecturerId).FirstOrDefault();
            var positions = Database.Positions.Where(x => positionIds.Any(y => y == x.Id)).ToList();
            var departments = Database.Departments.Where(x => departmentIds.Any(y => y == x.Id)).ToList();

            lecturer.Firstname = firstName;
            lecturer.Middlename = middleName;
            lecturer.Lastname = lastName;
            lecturer.Contacts = contacts;
            lecturer.Positions = positions;
            lecturer.Departments = departments;
            lecturer.IsActual = true;
            lecturer.CreatedDate = DateTime.Now;
            lecturer.UpdatedDate = DateTime.Now;

            Database.Update(lecturer);
        }

        public void CreateLecturer(
               string firstName,
               string middleName,
               string lastName,
               string contacts,
               int[] positionIds,
               int[] departmentIds
            )
        {
            var positions = Database.Positions.Where(x => positionIds.Any(y => y == x.Id)).ToList();
            var departments = Database.Departments.Where(x => departmentIds.Any(y => y == x.Id)).ToList();

            Database.Lecturers.Add(
                    new Lecturer
                    {
                        Firstname = firstName,
                        Middlename = middleName,
                        Lastname = lastName,
                        Contacts = contacts,
                        Positions = positions,
                        Departments = departments,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteLecturer(int lecturerId)
        {
            var lecturer = Database.Lecturers.Where(x => x.Id == lecturerId).FirstOrDefault();
            Database.Lecturers.Remove(lecturer);
            Database.SaveChanges();
        }

        public void EditTutorial(
           string name,
           string shortName,
           int tutorialId
           )
        {
            var tutorial = Database.Tutorials.Where(x => x.Id == tutorialId).FirstOrDefault();

            tutorial.Name = name;
            tutorial.ShortName = shortName;
            tutorial.IsActual = true;
            tutorial.CreatedDate = DateTime.Now;
            tutorial.UpdatedDate = DateTime.Now;

            Database.Update(tutorial);
        }

        public void CreateTutorial(
               string name,
               string shortName
            )
        {

            Database.Tutorials.Add(
                    new Tutorial
                    {
                        Name = name,
                        ShortName = shortName,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteTutorial(int tutorialId)
        {
            var tutorial = Database.Tutorials.Where(x => x.Id == tutorialId).FirstOrDefault();
            Database.Tutorials.Remove(tutorial);
            Database.SaveChanges();
        }

        public void EditGroup(
          string code,
          int studentsCount,
          int [] facultyIds,
          int [] courseIds,
          int studyTypeId,
          int groupId,
          bool isActual)
        {

            var studyType = Database.StudyTypes.Where(x => x.Id == studyTypeId).FirstOrDefault();
            var faculties = Database.Faculties.Where(x => facultyIds.Any(y => y == x.Id)).ToList();
            var courses = Database.Courses.Where(x => courseIds.Any(y => y == x.Id)).ToList();
            
            var group = Database.Groups.Where(x => x.Id == groupId).FirstOrDefault();

            group.Code = code;
            group.StudentsCount = studentsCount;
            //group.Faculties = faculties;
            //group.Courses = courses;
            //group.StudyType = studyType;
            group.IsActual = isActual;
            group.CreatedDate = DateTime.Now;
            group.UpdatedDate = DateTime.Now;

            Database.Update(group);
        }

        public void CreateGroup(
                string code,
                int studentsCount,
                int [] facultyIds,
                int [] courseIds,
                int studyTypeId,
                bool isActual)
        {
            var studyType = Database.StudyTypes.Where(x => x.Id == studyTypeId).FirstOrDefault();
            var faculties = Database.Faculties.Where(x => facultyIds.Any(y => y == x.Id)).ToList();
            var courses = Database.Courses.Where(x => courseIds.Any(y => y == x.Id)).ToList();

            Database.Groups.Add(
                    new Timetable.Data.Models.Scheduler.Group
                    {
                        Code = code,
                        StudentsCount = studentsCount,
                        Faculties = faculties,
                        Courses = courses,
                        StudyType = studyType,
                        IsActual = isActual,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteGroup(int groupId)
        {
            var group = Database.Groups.Where(x => x.Id == groupId).FirstOrDefault();
            Database.Groups.Remove(group);
            Database.SaveChanges();
        }

        public void EditScheduleInfo(
          int subGroupCount,
          decimal hoursPerWeek,
          string startDate,
          string endDate,
          int[] facultyIds,
          int[] courseIds,
          int[] groupIds,
          string lecturerSearchString,
          int semesterId,
          int departmentId,
          int studyYearId,
          string tutorialSearchString,
          int tutorialTypeId,
          int scheduleInfoId
          )
        {
      
            var studyYear = Database.StudyYears.Where(x => x.Id == studyYearId).FirstOrDefault();
            var department = Database.Departments.Where(x => x.Id == departmentId).FirstOrDefault();
            var tutorialType = Database.TutorialTypes.Where(x => x.Id == tutorialTypeId).FirstOrDefault();
            var semester = Database.Semesters.Where(x => x.Id == semesterId).FirstOrDefault();
            var faculties = Database.Faculties.Where(x => facultyIds.Any(y => y == x.Id)).ToList();
            var courses = Database.Courses.Where(x => courseIds.Any(y => y == x.Id)).ToList();
            var groups = Database.Groups.Where(x => groupIds.Any(y => y == x.Id)).ToList();

            //TODO: нормальная загрузка преподавателя и предмета
            var tutorial = Database.Tutorials.Where(x => x.Name.Contains(tutorialSearchString)).FirstOrDefault();
            var lecturer = Database.Lecturers.Where(x => x.Lastname.Contains(lecturerSearchString)).FirstOrDefault();


            var scheduleInfo = Database.ScheduleInfoes.Where(x => x.Id == scheduleInfoId).FirstOrDefault();

            scheduleInfo.SubgroupCount = subGroupCount;
            scheduleInfo.HoursPerWeek = hoursPerWeek;
            scheduleInfo.StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            scheduleInfo.EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //scheduleInfo.Faculties = faculties;
            //scheduleInfo.Courses = courses;
            //scheduleInfo.Groups = groups;
            scheduleInfo.StudyYear = studyYear;
            scheduleInfo.Department = department;
            scheduleInfo.Tutorial = tutorial;
            scheduleInfo.TutorialType = tutorialType;
            scheduleInfo.Lecturer = lecturer;
            scheduleInfo.Semester = semester;

            scheduleInfo.IsActual = true;
            scheduleInfo.CreatedDate = DateTime.Now;
            scheduleInfo.UpdatedDate = DateTime.Now;

            Database.Update(scheduleInfo);
        }

        public void CreateScheduleInfo(
                int subGroupCount,
                decimal hoursPerWeek,
                string startDate,
                string endDate,
                int[] facultyIds,
                int[] courseIds,
                int[] groupIds,
                string lecturerSearchString,
                int semesterId,
                int departmentId,
                int studyYearId,
                string tutorialSearchString,
                int tutorialTypeId
            )
        {
            var studyYear = Database.StudyYears.Where(x => x.Id == studyYearId).FirstOrDefault();
            var department = Database.Departments.Where(x => x.Id == departmentId).FirstOrDefault();
            var tutorialType = Database.TutorialTypes.Where(x => x.Id == tutorialTypeId).FirstOrDefault();
            var semester = Database.Semesters.Where(x => x.Id == semesterId).FirstOrDefault();
            var faculties = Database.Faculties.Where(x => facultyIds.Any(y => y == x.Id)).ToList();
            var courses = Database.Courses.Where(x => courseIds.Any(y => y == x.Id)).ToList();
            var groups = Database.Groups.Where(x => groupIds.Any(y => y == x.Id)).ToList();

            var tutorial = Database.Tutorials.Where(x => x.Name.Contains(tutorialSearchString)).FirstOrDefault();
            var lecturer = Database.Lecturers.Where(x => x.Lastname.Contains(lecturerSearchString)).FirstOrDefault();

        

            Database.ScheduleInfoes.Add(
                    new ScheduleInfo
                    {
                        SubgroupCount = subGroupCount,
                        HoursPerWeek = hoursPerWeek,
                        StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Courses = courses,
                        Faculties = faculties,
                        Groups = groups,
                        StudyYear = studyYear,
                        Department = department,
                        Tutorial = tutorial,
                        TutorialType = tutorialType,
                        Lecturer = lecturer,
                        Semester = semester,

                        IsActual = true,
                        IsPlanned = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteScheduleInfo(int scheduleInfoId)
        {
            var scheduleInfo = Database.ScheduleInfoes.Where(x => x.Id == scheduleInfoId).FirstOrDefault();
            Database.ScheduleInfoes.Remove(scheduleInfo);
            Database.SaveChanges();
        }

        public void EditSchedule(
            bool autoDelete,
            int dayOfWeek,
            string subGroup,
            string startDate,
            string endDate,
            int auditoriumId,
            int scheduleInfoId,
            int timeId,
            int scheduleTypeId,
            int weekTypeId,
            int scheduleId
          )
        {

            var auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId).FirstOrDefault();
            var scheduleInfo = Database.ScheduleInfoes.Where(x => x.Id == scheduleInfoId).FirstOrDefault();
            var time = Database.Times.Where(x => x.Id == timeId).FirstOrDefault();
            var scheduleType = Database.ScheduleTypes.Where(x => x.Id == scheduleTypeId).FirstOrDefault();
            var weekType = Database.WeekTypes.Where(x => x.Id == weekTypeId).FirstOrDefault();

            var schedule = Database.Schedules.Where(x => x.Id == scheduleId).FirstOrDefault();

            schedule.AutoDelete = autoDelete;
            schedule.DayOfWeek = dayOfWeek;
            schedule.SubGroup = subGroup;
            schedule.StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            schedule.EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            schedule.Auditorium = auditorium;
            schedule.ScheduleInfo = scheduleInfo;
            schedule.Time = time;
            schedule.Type = scheduleType;
            schedule.WeekType = weekType;
            schedule.IsActual = true;
            schedule.CreatedDate = DateTime.Now;
            schedule.UpdatedDate = DateTime.Now;

            Database.Update(schedule);
        }

        public void CreateSchedule(
                bool autoDelete,
                int dayOfWeek,
                string subGroup,
                string startDate,
                string endDate,
                int auditoriumId,
                int scheduleInfoId,
                int timeId,
                int scheduleTypeId,
                int weekTypeId
            )
        {
            var auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId).FirstOrDefault();
            var scheduleInfo = Database.ScheduleInfoes.Where(x => x.Id == scheduleInfoId).FirstOrDefault();
            var time = Database.Times.Where(x => x.Id == timeId).FirstOrDefault();
            var scheduleType = Database.ScheduleTypes.Where(x => x.Id == scheduleTypeId).FirstOrDefault();
            var weekType = Database.WeekTypes.Where(x => x.Id == weekTypeId).FirstOrDefault();


            Database.Schedules.Add(
                    new Schedule
                    {
                        AutoDelete = autoDelete,
                        DayOfWeek = dayOfWeek,
                        SubGroup = subGroup,
                        StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),

                        Auditorium = auditorium,
                        ScheduleInfo = scheduleInfo,
                        Time = time,
                        Type = scheduleType,
                        WeekType = weekType,
                        IsActual = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                );

            Database.SaveChanges();
        }

        public void DeleteSchedule(int scheduleId)
        {
            var schedule = Database.Schedules.Where(x => x.Id == scheduleId).FirstOrDefault();
            Database.Schedules.Remove(schedule);
            Database.SaveChanges();
        }


        public AuditoriumOrderDataTransfer PlanAuditoriumOrder(
                string tutorialName,
                string lecturerName,
                string threadName,
                int dayOfWeek,
                int timeId,
                int auditoriumId,
                bool autoDelete
            )
        {

            var auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId).FirstOrDefault();
            var time = Database.Times.Where(x => x.Id == timeId).FirstOrDefault();


            var auditoriumOrder = new AuditoriumOrder
                    {
                        TutorialName = tutorialName,
                        LecturerName = lecturerName,
                        ThreadName = threadName,
                        DayOfWeek = dayOfWeek,
                        Time = time,
                        Auditorium = auditorium,
                        AutoDelete = autoDelete,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsActual = true
                    };
            Database.AuditoriumOrders.Add(
                    auditoriumOrder
                );
            Database.SaveChanges();

            return new AuditoriumOrderDataTransfer(auditoriumOrder);
        }

        public void EditAuditoriumOrder(
                string tutorialName,
                string lecturerName,
                string threadName,
                int auditoriumOrderId,
                bool autoDelete
            )
        {

            var auditoriumOrder = Database.AuditoriumOrders.Where(x => x.Id == auditoriumOrderId).FirstOrDefault();

            auditoriumOrder.ThreadName = threadName;
            auditoriumOrder.LecturerName = lecturerName;
            auditoriumOrder.TutorialName = tutorialName;
            auditoriumOrder.AutoDelete = autoDelete;

            Database.Update(auditoriumOrder);
        }

        public void UnplanAuditoriumOrder(
                int auditoriumOrderId
            )
        {
            var auditoriumOrder = Database.AuditoriumOrders.Where(x => x.Id == auditoriumOrderId).FirstOrDefault();
            Database.AuditoriumOrders.Remove(auditoriumOrder);
            Database.SaveChanges();
        }

        public IEnumerable<AuditoriumOrderDataTransfer> GetAuditoriumOrders(
            int timeId, 
            int dayOfWeek, 
            int buildingId)
        {
            var result = Database.AuditoriumOrders.Where(
                x => x.Time.Id == timeId &&
                x.DayOfWeek == dayOfWeek &&
                x.Auditorium.Building.Id == buildingId).AsQueryable();

            return result
                .ToList()
                .Select(x => new AuditoriumOrderDataTransfer(x));
        }


        
 
        public IEnumerable<AuditoriumOrderDataTransfer> GetAuditoriumOrders(
            int timeId,
            int dayOfWeek,
            int buildingId,
            int[] auditoriumTypeIds = null)
        {
            var result = Database.AuditoriumOrders.Where(
                x => x.Time.Id == timeId &&
                x.DayOfWeek == dayOfWeek &&
                x.Auditorium.Building.Id == buildingId &&
                auditoriumTypeIds.Contains(x.Auditorium.AuditoriumType.Id)).AsQueryable();

       
            return result
                .ToList()
                .Select(x => new AuditoriumOrderDataTransfer(x));
        }

        public void PlanExam(
                int? lecturerId,
                int tutorialId,
                int? groupId,
                int? auditoriumId,
                DateTime time
            )
        {
            Data.Models.Scheduler.Auditorium auditorium = null;
            Data.Models.Scheduler.Lecturer lecturer = null;
            Data.Models.Scheduler.Group group = null;

            if(auditoriumId.HasValue)
                auditorium = Database.Auditoriums.Where(x => x.Id == auditoriumId.Value).FirstOrDefault();

            if (lecturerId.HasValue)
                lecturer = Database.Lecturers.Where(x => x.Id == lecturerId.Value).FirstOrDefault();

            if (groupId.HasValue)
                group = Database.Groups.Where(x => x.Id == groupId.Value).FirstOrDefault();

            var tutorial = Database.Tutorials.Where(x => x.Id == tutorialId).FirstOrDefault();

            Database.Exams.Add(
                    new Exam
                    {
                        Tutorial = tutorial,
                        Lecturer = lecturer,
                        Group = group,
                        Auditorium = auditorium,
                        Time = time
                    }
                );
            Database.SaveChanges();
        }

        public void UnplanExam(
                int examId
            )
        {
            var exam = Database.Exams.Where(x => x.Id == examId).FirstOrDefault();
            Database.Exams.Remove(exam);
            Database.SaveChanges();
        }

        public IEnumerable<ExamDataTransfer> GetExams(
            int LecturerId
            )
        {
            var result = Database.Exams.Where(
                x => x.LecturerId == LecturerId).AsQueryable();

            return result
                .ToList()
                .Select(x => new ExamDataTransfer(x));
        }
    }
}
