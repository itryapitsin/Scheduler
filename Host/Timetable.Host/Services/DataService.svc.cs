using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Timetable.Data.Context.Interfaces;
using Timetable.Data.Models.Scheduler;
using Timetable.Host.Interfaces;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Services
{
    public class DataService : BaseService<ISchedulerDatabase>, IDataService
    {
        public IQueryable<TimetableEntityDataTransfer> GetTimetableEntities()
        {
            return Database.TimetableEntities
                .Where(x => x.IsActual)
                .Select(x => new TimetableEntityDataTransfer(x));
        }

        public bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer)
        {
            var schedulesCount = 0;
            var schedules = Database.Schedules
                .Where(x => x.IsActual 
                        && x.AuditoriumId == scheduleDataTransfer.AuditoriumId 
                        && x.PeriodId == scheduleDataTransfer.PeriodId 
                        && x.DayOfWeek == scheduleDataTransfer.DayOfWeek 
                        && (x.StartDate >= scheduleDataTransfer.StartDate && x.StartDate <= scheduleDataTransfer.EndDate 
                        || x.EndDate >= scheduleDataTransfer.StartDate && x.EndDate <= scheduleDataTransfer.EndDate));


            if (scheduleDataTransfer.WeekTypeId == 1)
                schedulesCount = schedules.Count();

            if (scheduleDataTransfer.WeekTypeId == 2)
                schedulesCount = schedules.Count(x => x.WeekTypeId == 1 || x.WeekTypeId == 2);

            if (scheduleDataTransfer.WeekTypeId == 3)
                schedulesCount = schedules.Count(x => x.WeekTypeId == 1 || x.WeekTypeId == 3);

            return schedulesCount == 0;
        }

        public IQueryable<BranchDataTransfer> GetBranches()
        {
            return Database.Branches
                .Where(x => x.IsActual)
                .Select(x => new BranchDataTransfer(x));
        }

        public IQueryable<AuditoriumDataTransfer> GetAuditoriums(Models.Scheduler.BuildingDataTransfer buildingDataTransfer, AuditoriumTypeDataTransfer auditoriumTypeDataTransfer)
        {
            if (auditoriumTypeDataTransfer != null)
            {
                // TODO: Need tutorial type reference
                return Database.Auditoriums
                    .Where(x => x.Building.Id.Equals(buildingDataTransfer.Id))
                    .Where(x => x.AuditoriumType.Id == auditoriumTypeDataTransfer.Id)
                    .Include(x => x.Building)
                    .Select(x => new AuditoriumDataTransfer(x));
            }
            else
            {
                return Database.Auditoriums
                    .Where(x => x.Building.Id.Equals(buildingDataTransfer.Id))
                    .Where(x => x.AuditoriumType == null)
                    .Include(x => x.Building)
                    .Select(x => new AuditoriumDataTransfer(x));
            }

        }

        public IQueryable<AuditoriumDataTransfer> GetFreeAuditoriums(
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
           
            IQueryable<AuditoriumDataTransfer> freeAuditoriums;
            IQueryable<AuditoriumDataTransfer> scheduledAuditoriums;

            if (weekTypeDataTransfer.Id == 1)
            {
                scheduledAuditoriums = Database.Schedules
                     .Where(x => x.IsActual)
                     .Where(x => x.StartDate <= endDate && x.EndDate >= startDate) 
                     .Where(x => x.Period.Id == timeDataTransfer.Id)
                     .Where(x => x.DayOfWeek == dayOfWeek)
                     .Where(x => (x.WeekType.Id == weekTypeDataTransfer.Id || x.WeekType.Id == 2 || x.WeekType.Id == 3))
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
                     .Select(x => new AuditoriumDataTransfer(x.Auditorium));
            }



            if (tutorialTypeDataTransfer == null)
            {
                freeAuditoriums = Database.Auditoriums
                    .Where(x => x.Building.Id == buildingDataTransfer.Id)
                    .Where(x => x.Capacity >= capacity)
                    .Where(x => x.AuditoriumType.Id == auditoriumTypeDataTransfer.Id)
                    .Where(x => !scheduledAuditoriums.Any(y => y.Id == x.Id))
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
                    .Select(x => new AuditoriumDataTransfer(x));

            }

            return freeAuditoriums;
        }

        public IQueryable<BuildingDataTransfer> GetBuildings()
        {
            return Database.Buildings.Select(x => new BuildingDataTransfer(x));
        }

        public IQueryable<CourseDataTransfer> GetCources()
        {
            return Database.Courses.Where(x => x.IsActual).Select(x => new CourseDataTransfer(x));
        }

        public IQueryable<DepartmentDataTransfer> GetDeparmtents()
        {
            return Database.Departments.Select(x => new DepartmentDataTransfer(x));
        }


        public IQueryable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null)
        {
            if (branchDataTransfer == null)
                return Database.Faculties
                    .Where(x => x.IsActual)
                    .Where(x => x.Branch == null)
                    .Select(x => new FacultyDataTransfer(x));

            return Database.Faculties
                .Where(x => x.IsActual)
                .Where(x => x.BranchId == branchDataTransfer.Id)
                .Select(x => new FacultyDataTransfer(x));
        }

        public GroupDataTransfer GetGroupById(int groupId)
        {
            return new GroupDataTransfer(
                Database.Groups.FirstOrDefault(x => x.Id == groupId));
        }

        public IQueryable<GroupDataTransfer> GetGroupsByCode(string code, int count)
        {
            return Database.Groups
                .Where(x => x.Code.Contains(code))
                .Take(count)
                .Select(x => new GroupDataTransfer(x));
        }

        public IQueryable<GroupDataTransfer> GetsSubGroupsByGroupId(int groupId)
        {
            return Database.Groups
                .Where(x => x.Parent.Id.Equals(groupId))
                .Select(x => new GroupDataTransfer(x));
        }

        public IQueryable<GroupDataTransfer> GetGroups(FacultyDataTransfer facultyDataTransfer, CourseDataTransfer courseDataTransfer)
        {
            return Database.Groups
                .Where(x => x.IsActual)
                .Where(x => x.Course.Id.Equals(courseDataTransfer.Id))
                .Where(x => x.Speciality.Faculties
                    .Any(y => y.Id.Equals(facultyDataTransfer.Id)))
                .Select(x => new GroupDataTransfer(x));
        }

        public IQueryable<GroupDataTransfer> GetGroups(CourseDataTransfer courseDataTransfer, SpecialityDataTransfer specialityDataTransfer)
        {
            return Database.Groups
                .Where(x => x.IsActual)
                .Where(x => x.Speciality.Id.Equals(specialityDataTransfer.Id))
                .Where(x => x.Course.Id.Equals(courseDataTransfer.Id))
                .Select(x => new GroupDataTransfer(x));
        }

        public IQueryable<LecturerDataTransfer> GetLecturersByDeparmentId(DepartmentDataTransfer departmentDataTransfer)
        {
            return Database.Lecturers
                .Where(x => x.Departments
                    .Any(y => y.Id.Equals(departmentDataTransfer.Id)))
                .Select(x => new LecturerDataTransfer(x));
        }

        public IQueryable<LecturerDataTransfer> GetLecturersByTutorialId(TutorialDataTransfer tutorialDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Tutorial.Id.Equals(tutorialDataTransfer.Id))
                .Select(x => new LecturerDataTransfer(x.Lecturer));
        }

        public IQueryable<LecturerDataTransfer> GetLecturersByTutorialIdAndTutorialTypeId(
            TutorialDataTransfer tutorialDataTransfer,
            TutorialTypeDataTransfer tutorialTypeDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Tutorial.Id.Equals(tutorialDataTransfer.Id))
                .Where(x => x.TutorialType.Id.Equals(tutorialTypeDataTransfer.Id))
                .Select(x => new LecturerDataTransfer(x.Lecturer));
        }


        public LecturerDataTransfer GetLecturerByFirstMiddleLastname(string arg)
        {
            return GetLecturersByFirstMiddleLastname(arg)
                .FirstOrDefault();
        }

        public IQueryable<LecturerDataTransfer> GetLecturersByFirstMiddleLastname(string arg)
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

        public ScheduleInfoDataTransfer GetScheduleInfoById(int id)
        {
            return new ScheduleInfoDataTransfer(
                GetScheduleInfoes().FirstOrDefault(scheduleInfo => scheduleInfo.Id == id));
        }

        public IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForCourse(
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
                .Select(x => new ScheduleInfoDataTransfer(x));
        }

        public IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester)
        {
            return GetScheduleInfoesForCourse(facultyDataTransfer, courseDataTransfer, studyYearDataTransfer, semester)
                .Where(x => x.Specialities.Any(y => y.Id.Equals(specialityDataTransfer.Id)));
        }

        public IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer,
            TutorialTypeDataTransfer tutorialtype,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester)
        {
            return GetScheduleInfoes()
                .Where(x => x.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.Semester == semester)
                .Where(x => x.TutorialType.Id.Equals(tutorialtype.Id))
                .Select(x => new ScheduleInfoDataTransfer(x));
            //.Where(x => x.Groups.Any(y => groups.Any(z => z.Id.Equals(y.Id))))
        }



        public IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForDepartment(
            DepartmentDataTransfer departmentDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester)
        {
            return GetScheduleInfoes()
                .Where(x => x.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.Semester == semester)
                .Where(x => x.Department.Id.Equals(departmentDataTransfer.Id))
                .Select(x => new ScheduleInfoDataTransfer(x));
        }

        public IQueryable<ScheduleInfoDataTransfer> GetUnscheduledInfoes(
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

            return test.Select(x => new ScheduleInfoDataTransfer(x));
        }

        private IQueryable<ScheduleInfo> GetScheduleInfoes()
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

        public IQueryable<AuditoriumTypeDataTransfer> GetAuditoriumTypes()
        {
            return Database.AuditoriumTypes.Select(x => new AuditoriumTypeDataTransfer(x));
        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForAll(
            LecturerDataTransfer lecturerDataTransfer, 
            AuditoriumDataTransfer auditoriumDataTransfer,
            IEnumerable<GroupDataTransfer> groups,
            WeekTypeDataTransfer weekTypeDataTransfer,
            string subGroup,
            DateTime startDate,
            DateTime endDate)
        {
            var result = GetSchedules();
            if(lecturerDataTransfer != null)
                result = result.Where(x => x.ScheduleInfo.Lecturer.Id == lecturerDataTransfer.Id);
            if(auditoriumDataTransfer != null)
                result = result.Where(x => x.Auditorium.Id == auditoriumDataTransfer.Id);

            foreach(var group in groups)
                    result = result.Where(x => x.ScheduleInfo.Groups.Any(y => y.Id == group.Id));

            if(subGroup != null)
                    result = result.Where(x => x.SubGroup == null || x.SubGroup == subGroup);

            if(startDate != null)
                    result = result.Where(x => x.EndDate >= startDate);

            if(endDate != null)
                    result = result.Where(x => x.StartDate <= endDate);

            if(weekTypeDataTransfer != null)
                    if(weekTypeDataTransfer.Id == 2)
                        result = result.Where(x => x.WeekType.Id != 3);
                    else if(weekTypeDataTransfer.Id == 3)
                        result = result.Where(x => x.WeekType.Id != 2);
                    
            //TODO: order by priority
            var query = result.GroupBy(x => new { x.DayOfWeek, x.Period.Id });

            //TODO: improuve speed
            var answer = new List<ScheduleDataTransfer>();
            foreach(var q in query)
                answer.Add(new ScheduleDataTransfer(q.OrderBy(x => x.CreatedDate).First()));

            return answer.AsQueryable();
        }


        public IQueryable<ScheduleDataTransfer> GetSchedulesForDayTimeDate(
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

            if(period != null)
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
            return result.OrderBy(x => x.CreatedDate).Select(x => new ScheduleDataTransfer(x));
        }

        public int CountScheduleCollisions(
            int day,
            TimeDataTransfer timeDataTransfer,
            WeekTypeDataTransfer weekTypeDataTransfer)
        {
            return Database.Schedules.Count(x => x.Period.Id.Equals(timeDataTransfer.Id) && x.DayOfWeek.Equals(day) &&
                (x.WeekType.Id == 1 || x.WeekType.Id == weekTypeDataTransfer.Id));
        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesByDayTime(int day, TimeDataTransfer timeDataTransfer)
        {
            return GetSchedules()
                .Where(x => (x.DayOfWeek == day && x.Period.Id == timeDataTransfer.Id))
                .Select(x => new ScheduleDataTransfer(x));
        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime StartDate,
            DateTime EndDate)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id.Equals(facultyDataTransfer.Id))
                            && x.ScheduleInfo.Courses.Any(y => y.Id.Equals(courseDataTransfer.Id)))
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate,
            string subGroup)
        {
            var result =  GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id.Equals(facultyDataTransfer.Id))
                            && x.ScheduleInfo.Courses.Any(y => y.Id.Equals(courseDataTransfer.Id))
                            && x.ScheduleInfo.Groups.Any(y => y.Id.Equals(groupDataTransfer.Id)));
            if (subGroup != null)
                result = result.Where(x => x.SubGroup == subGroup);

            return result.Select(x => new ScheduleDataTransfer(x));
        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForGroupOnlyId(
           GroupDataTransfer groupDataTransfer,
           StudyYearDataTransfer studyYearDataTransfer,
           int semester,
           DateTime startDate,
           DateTime endDate)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.StartDate <= endDate && x.EndDate >= startDate)
                .Where(x => x.ScheduleInfo.Groups.Any(y => y.Id.Equals(groupDataTransfer.Id)))
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Faculties.Any(y => y.Id.Equals(facultyDataTransfer.Id))
                            && x.ScheduleInfo.Courses.Any(y => y.Id.Equals(courseDataTransfer.Id))
                            && x.ScheduleInfo.Specialities.Any(y => y.Id.Equals(specialityDataTransfer.Id)))
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForLecturer(
            LecturerDataTransfer lecturerDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate)
        {
            var result =  GetSchedules()
                .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearDataTransfer.Id)
                .Where(x => x.ScheduleInfo.Semester == semester)
                .Where(x => x.ScheduleInfo.Lecturer.Id.Equals(lecturerDataTransfer.Id))
                .Select(x => new ScheduleDataTransfer(x));

            return result;
        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            AuditoriumDataTransfer auditoriumDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate)
        {
            var result = GetSchedules()
               .Where(x => x.ScheduleInfo.StudyYear.Id == studyYearDataTransfer.Id)
               .Where(x => x.ScheduleInfo.Semester == semester)
               .Where(x => x.Auditorium.Id.Equals(auditoriumDataTransfer.Id))
               .Select(x => new ScheduleDataTransfer(x));
         
            return result;
        }


        public int CountSchedulesForScheduleInfoes(ScheduleInfoDataTransfer scheduleInfoDataTransfer)
        {
            return Database.Schedules.Count(x => x.ScheduleInfo.Id.Equals(scheduleInfoDataTransfer.Id));
        }

        public IQueryable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(ScheduleInfoDataTransfer scheduleInfoDataTransfer)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.Id.Equals(scheduleInfoDataTransfer.Id))
                .Select(x => new ScheduleDataTransfer(x));
        }

        public ScheduleDataTransfer GetScheduleById(int id)
        {
            return new ScheduleDataTransfer(
                GetSchedules().FirstOrDefault(schedule => schedule.Id == id));

        }

        private IQueryable<Schedule> GetSchedules()
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

        public IQueryable<TutorialDataTransfer> GetTutorialsForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
                .Where(x => x.Groups.Any(y => y.Id == groupDataTransfer.Id))
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IQueryable<TutorialDataTransfer> GetTutorialsForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
                .Where(x => x.Specialities.Any(y => y.Id == specialityDataTransfer.Id))
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IQueryable<TutorialDataTransfer> GetTutorialsForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IQueryable<SpecialityDataTransfer> GetSpecialities(FacultyDataTransfer facultyDataTransfer)
        {
            return Database.Specialities
                .Where(x => x.Faculties
                    .Any(y => y.Id.Equals(facultyDataTransfer.Id)))
                .Select(x => new SpecialityDataTransfer(x));
        }

        public IQueryable<TimeDataTransfer> GetTimes(BuildingDataTransfer buildingDataTransfer)
        {
            return Database.Times.Select(x => new TimeDataTransfer(x));
            //.Where(x => x.Building.Id.Equals(Building.Id))
            //.Include(x => x.Building);
        }

        public IQueryable<TutorialTypeDataTransfer> GetTutorialTypes()
        {
            return Database.TutorialTypes.Select(x => new TutorialTypeDataTransfer(x));
        }

        public TutorialTypeDataTransfer GetTutorialTypeById(TutorialTypeDataTransfer tutorialTypeDataTransfer)
        {
            return new TutorialTypeDataTransfer(
                Database.TutorialTypes.FirstOrDefault(x => x.Id == tutorialTypeDataTransfer.Id));
        }

        public IQueryable<WeekTypeDataTransfer> GetWeekTypes()
        {
            return Database.WeekTypes.Select(x => new WeekTypeDataTransfer(x));
        }

        public IQueryable<StudyYearDataTransfer> GetStudyYears()
        {
            return Database.StudyYears.Select(x => new StudyYearDataTransfer(x));
        }
    }
}
