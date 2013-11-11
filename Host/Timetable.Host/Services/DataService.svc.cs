﻿using System;
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
        public GroupDataTransfer GetGroupById(int groupId)
        {
            return new GroupDataTransfer(
                Database.Groups.FirstOrDefault(x => x.Id == groupId));
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
                .Where(x => x.Speciality.Faculties
                    .Any(y => y.Id.Equals(facultyId)))
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

        public IEnumerable<LecturerDataTransfer> GetLecturersByDeparmentId(DepartmentDataTransfer departmentDataTransfer)
        {
            return Database.Lecturers
                .Where(x => x.Departments
                    .Any(y => y.Id.Equals(departmentDataTransfer.Id)))
                .ToList()
                .Select(x => new LecturerDataTransfer(x));
        }

        public IEnumerable<LecturerDataTransfer> GetLecturersByTutorialId(TutorialDataTransfer tutorialDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Tutorial.Id.Equals(tutorialDataTransfer.Id))
                .ToList()
                .Select(x => new LecturerDataTransfer(x.Lecturer));
        }

        public IEnumerable<LecturerDataTransfer> GetLecturersByTutorialIdAndTutorialTypeId(
            TutorialDataTransfer tutorialDataTransfer,
            TutorialTypeDataTransfer tutorialTypeDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Tutorial.Id.Equals(tutorialDataTransfer.Id))
                .Where(x => x.TutorialType.Id.Equals(tutorialTypeDataTransfer.Id))
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

        public IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroup(
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
                .ToList()
                .Select(x => new ScheduleInfoDataTransfer(x));
            //.Where(x => x.Groups.Any(y => groups.Any(z => z.Id.Equals(y.Id))))
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

        public IEnumerable<ScheduleDataTransfer> GetSchedulesByDayTime(int day, TimeDataTransfer timeDataTransfer)
        {
            return GetSchedules()
                .Where(x => (x.DayOfWeek == day && x.Period.Id == timeDataTransfer.Id))
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForCourse(
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
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForGroup(
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

            return result.ToList().Select(x => new ScheduleDataTransfer(x));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForGroupOnlyId(
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
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForSpeciality(
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
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));

        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForLecturer(
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
                .ToList()
                .Select(x => new ScheduleDataTransfer(x));

            return result;
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
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
               .ToList()
               .Select(x => new ScheduleDataTransfer(x));
         
            return result;
        }


        public int CountSchedulesForScheduleInfoes(ScheduleInfoDataTransfer scheduleInfoDataTransfer)
        {
            return Database.Schedules.Count(x => x.ScheduleInfo.Id.Equals(scheduleInfoDataTransfer.Id));
        }

        public IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(ScheduleInfoDataTransfer scheduleInfoDataTransfer)
        {
            return GetSchedules()
                .Where(x => x.ScheduleInfo.Id.Equals(scheduleInfoDataTransfer.Id))
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
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
                .Where(x => x.Specialities.Any(y => y.Id == specialityDataTransfer.Id))
                .ToList()
                .Select(x => new TutorialDataTransfer(x.Tutorial));
        }

        public IEnumerable<TutorialDataTransfer> GetTutorialsForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer)
        {
            return Database.ScheduleInfoes
                .Where(x => x.Faculties.Any(y => y.Id == facultyDataTransfer.Id))
                .Where(x => x.Courses.Any(y => y.Id == courseDataTransfer.Id))
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

        public IEnumerable<TimeDataTransfer> GetTimes(BuildingDataTransfer buildingDataTransfer)
        {
            return Database.Times.ToList().Select(x => new TimeDataTransfer(x));
            //.Where(x => x.Building.Id.Equals(Building.Id))
            //.Include(x => x.Building);
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
