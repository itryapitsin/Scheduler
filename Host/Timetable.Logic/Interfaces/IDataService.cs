using System;
using System.Collections.Generic;
using Timetable.Logic.Exceptions;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Logic.Interfaces
{
    public interface IDataService : IBaseService
    {
        IEnumerable<SemesterDataTransfer> GetSemesters();

        IEnumerable<GroupDataTransfer> GetGroupsByCode(string code, int count);
        bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer);
        IEnumerable<BranchDataTransfer> GetBranches();
        AuditoriumDataTransfer GetAuditoriumById(int auditoriumId); 
        IEnumerable<AuditoriumDataTransfer> GetAuditoriums(
            int buildingId,
            int[] auditoriumTypeIds = null,
            bool? isTraining = null);

        IEnumerable<AuditoriumDataTransfer> GetFreeAuditoriums(
            int buildingId,
            int dayOfWeek,
            int weekTypeId,
            int timeId);
        IEnumerable<AuditoriumTypeDataTransfer> GetAuditoriumTypes(bool? isTraining = null);
        IEnumerable<BuildingDataTransfer> GetBuildings();
        IEnumerable<CourseDataTransfer> GetCources(int branchId);
        IEnumerable<int> GetPairs();
        IEnumerable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null);
        IEnumerable<FacultyDataTransfer> GetFaculties(int branchId);

        #region groups
        IEnumerable<GroupDataTransfer> GetGroupsByIds(int[] groupIds);
        IEnumerable<GroupDataTransfer> GetsSubGroupsByGroupId(int groupId);

        IEnumerable<GroupDataTransfer> GetGroupsForFaculty(
            int facultyId,
            int courseId);

        IEnumerable<GroupDataTransfer> GetGroupsForFaculty(
            int facultyId, 
            int courseId, 
            int studyTypeId);

        #endregion

        #region lecturers
        LecturerDataTransfer GetLecturerById(int lecturerId);
        LecturerDataTransfer GetLecturerBySearchQuery(string queryString);
        IEnumerable<LecturerDataTransfer> GetLecturersByDeparmentId(int departmentId);
        IEnumerable<LecturerDataTransfer> GetLecturersByTutorialId(int tutorialId);
        IEnumerable<LecturerDataTransfer> GetLecturersByTutorialIdAndTutorialTypeId(
            int tutorialId,
            int tutorialTypeId);
        LecturerDataTransfer GetLecturerByFirstMiddleLastname(string arg);
        IEnumerable<LecturerDataTransfer> GetLecturersByFirstMiddleLastname(string arg);
        #endregion

        ScheduleInfoDataTransfer GetScheduleInfoById(int id);
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForFaculty(
            int facultyId,
            int courseId,
            int studyYear,
            int semester);

        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroups(
            int[] groupIds,
            int studyYear,
            int semester);

        IEnumerable<ScheduleInfoDataTransfer> GetUnscheduledInfoes(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            GroupDataTransfer groupDataTransfer);
        int CountScheduleCollisions(
            int day,
            int timeId,
            int weekTypeId);

        #region schedule

        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoes(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester);

        IEnumerable<ScheduleDataTransfer> GetSchedulesForGroups(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester);

        IEnumerable<ScheduleDataTransfer> GetSchedules(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester);
        IEnumerable<ScheduleDataTransfer> GetSchedules(
            int[] auditoriumIds,
            int studyYear,
            int semester);
        IEnumerable<ScheduleDataTransfer> GetSchedules(
            int auditoriumTypeId,
            int studyYear,
            int semester);
        IEnumerable<ScheduleDataTransfer> GetSchedulesForFaculty(
            int facultyId,
            int courseId,
            int studyYearId,
            int semester);
        int CountSchedulesForScheduleInfoes(int scheduleInfoId);

        IEnumerable<ScheduleDataTransfer> GetSchedulesForLecturer(
            int lecturerId,
            int studyYearId,
            int semester);
        IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            int auditoriumId,
            int studyYearId,
            int semester);
        IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            int auditoriumId,
            DateTime date);
        ScheduleDataTransfer GetScheduleById(int id);
        IEnumerable<ScheduleTypeDataTransfer> GetScheduleTypes();
        #endregion

        IEnumerable<TimeDataTransfer> GetTimesByIds(IEnumerable<int> timeIds);
        IEnumerable<SpecialityDataTransfer> GetSpecialities(int branchId);
        IEnumerable<SpecialityDataTransfer> GetSpecialitiesForFaculti(int facultyId);
        IEnumerable<TimeDataTransfer> GetTimes(int buildingId);
        TutorialDataTransfer GetTutorialById(TutorialDataTransfer tutorialDataTransfer);
        IEnumerable<TutorialDataTransfer> GetTutorialsForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer);
        IEnumerable<TutorialDataTransfer> GetTutorialsForSpeciality(
            int courseId,
            int specialityId);
        IEnumerable<TutorialDataTransfer> GetTutorialsForFaculty(
            int facultyId,
            int courseId);
        IEnumerable<TutorialTypeDataTransfer> GetTutorialTypes();
        TutorialTypeDataTransfer GetTutorialTypeById(TutorialTypeDataTransfer tutorialTypeDataTransfer);
        IEnumerable<WeekTypeDataTransfer> GetWeekTypes();
        IEnumerable<StudyYearDataTransfer> GetStudyYears();
        StudyYearDataTransfer GetStudyYear(DateTime date);
        IEnumerable<StudyTypeDataTransfer> GetStudyTypes();

        /// <summary>
        /// Запланировать предмет.
        /// </summary>
        /// <exception cref="ScheduleCollisionException"></exception>
        /// <param name="auditoriumId"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="scheduleInfoId"></param>
        /// <param name="timeId"></param>
        /// <param name="weekTypeId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        ScheduleDataTransfer Plan(
            int auditoriumId, 
            int dayOfWeek, 
            int scheduleInfoId, 
            int timeId, 
            int weekTypeId, 
            int typeId);
    }
}
