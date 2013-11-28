using System;
using System.Collections.Generic;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Logic.Interfaces
{
    public interface IDataService : IBaseService
    {
        IEnumerable<SemesterDataTransfer> GetSemesters();

        IEnumerable<ScheduleDataTransfer> GetSchedulesForDayTimeDate(
                                int? dayOfWeek,
                                TimeDataTransfer period,
                                WeekTypeDataTransfer weekTypeDataTransfer,
                                LecturerDataTransfer lecturerDataTransfer,
                                AuditoriumDataTransfer auditoriumDataTransfer,
                                IEnumerable<GroupDataTransfer> groups,
                                string subGroup,
                                DateTime startDate,
                                DateTime endDate
            );
        IEnumerable<ScheduleDataTransfer> GetSchedulesForAll(
                                LecturerDataTransfer lecturerDataTransfer,
                                AuditoriumDataTransfer auditoriumDataTransfer,
                                IEnumerable<GroupDataTransfer> groups,
                                WeekTypeDataTransfer weekTypeDataTransfer,
                                string subGroup,
                                DateTime startDate,
                                DateTime endDate
                                );
        IEnumerable<TimetableEntityDataTransfer> GetTimetableEntities();
        IEnumerable<GroupDataTransfer> GetGroupsByCode(string code, int count);
        bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer);
        IEnumerable<BranchDataTransfer> GetBranches();
        IEnumerable<AuditoriumDataTransfer> GetAuditoriums(
            int buildingId,
            int[] auditoriumTypeIds = null,
            bool? isTraining = null);
        IEnumerable<AuditoriumDataTransfer> GetAuditoriums(
            BuildingDataTransfer buildingDataTransfer,
            AuditoriumTypeDataTransfer auditoriumTypeDataTransfer);
        IEnumerable<AuditoriumDataTransfer> GetFreeAuditoriums(
            BuildingDataTransfer buildingDataTransfer,
            int day,
            WeekTypeDataTransfer weekTypeDataTransfer,
            TimeDataTransfer timeDataTransfer,
            TutorialTypeDataTransfer tutorialTypeDataTransfer,
            AuditoriumTypeDataTransfer auditoriumTypeDataTransfer,
            int capacity,
            DateTime startDate,
            DateTime endDate);
        IEnumerable<AuditoriumDataTransfer> GetFreeAuditoriums(
            int buildingId,
            int dayOfWeek,
            int weekTypeId,
            int timeId);
        IEnumerable<AuditoriumTypeDataTransfer> GetAuditoriumTypes(bool? isTraining = null);
        IEnumerable<BuildingDataTransfer> GetBuildings();
        IEnumerable<CourseDataTransfer> GetCources();
        IEnumerable<int> GetPairs();
        IEnumerable<DepartmentDataTransfer> GetDeparmtents();
        IEnumerable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null);
        IEnumerable<FacultyDataTransfer> GetFaculties(int branchId);

        #region groups
        IEnumerable<GroupDataTransfer> GetGroupsByIds(int[] groupIds);
        IEnumerable<GroupDataTransfer> GetsSubGroupsByGroupId(int groupId);
        IEnumerable<GroupDataTransfer> GetGroupsForFaculty(
            int facultyId,
            int[] courseIds);

        IEnumerable<GroupDataTransfer> GetGroupsForFaculty(
            int facultyId,
            int courseId);
        IEnumerable<GroupDataTransfer> GetGroupsForSpeciality(
            int specialityId,
            int[] courseIds);
        #endregion

        #region lecturers
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
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroups(
            int[] groupIds,
            int studyYear,
            int semester);
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForDepartment(
            DepartmentDataTransfer departmentDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);
        IEnumerable<ScheduleInfoDataTransfer> GetUnscheduledInfoes(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            GroupDataTransfer groupDataTransfer);
        int CountScheduleCollisions(
            int day,
            TimeDataTransfer timeDataTransfer,
            WeekTypeDataTransfer weekTypeDataTransfer);
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
        IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(int scheduleInfoId);
        IEnumerable<ScheduleDataTransfer> GetSchedulesByDayTime(
            int day,
            TimeDataTransfer timeDataTransfer);
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
        IEnumerable<ScheduleDataTransfer> GetSchedulesForSpeciality(
            int specialityId,
            int courseId,
            int studyYearId,
            int semester);
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
    }
}
