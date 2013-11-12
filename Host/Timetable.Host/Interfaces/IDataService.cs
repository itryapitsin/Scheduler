using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using Timetable.Data.Models.Scheduler;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Interfaces
{
    [ServiceContract]
    public interface IDataService : IBaseService
    {

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
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

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForAll(
                                LecturerDataTransfer lecturerDataTransfer,
                                AuditoriumDataTransfer auditoriumDataTransfer,
                                IEnumerable<GroupDataTransfer> groups,
                                WeekTypeDataTransfer weekTypeDataTransfer,
                                string subGroup,
                                DateTime startDate,
                                DateTime endDate
                                );


        //[OperationContract]
        //[WebGet(ResponseFormat = WebMessageFormat.Json)]
        //IEnumerable<ScheduleDataTransfer> GetSchedulesForGroupOnlyId(
        //   GroupDataTransfer groupDataTransfer,
        //   StudyYearDataTransfer studyYearDataTransfer,
        //   int semester,
        //   DateTime startDate,
        //   DateTime endDate);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TimetableEntityDataTransfer> GetTimetableEntities();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<GroupDataTransfer> GetGroupsByCode(string code, int count);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<BranchDataTransfer> GetBranches();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<AuditoriumDataTransfer> GetAuditoriums(
            BuildingDataTransfer buildingDataTransfer,
            AuditoriumTypeDataTransfer auditoriumTypeDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
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

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<AuditoriumTypeDataTransfer> GetAuditoriumTypes();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<BuildingDataTransfer> GetBuildings();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<CourseDataTransfer> GetCources();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<DepartmentDataTransfer> GetDeparmtents();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null);

        #region groups

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<GroupDataTransfer> GetGroupsByIds(int[] groupIds);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<GroupDataTransfer> GetsSubGroupsByGroupId(int groupId);


        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<GroupDataTransfer> GetGroupsForFaculty(
            int facultyId,
            int[] courseIds);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<GroupDataTransfer> GetGroupsForSpeciality(
            int specialityId,
            int[] courseIds);
        #endregion

        #region lecturers
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<LecturerDataTransfer> GetLecturersByDeparmentId(int departmentId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<LecturerDataTransfer> GetLecturersByTutorialId(int tutorialId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<LecturerDataTransfer> GetLecturersByTutorialIdAndTutorialTypeId(
            int tutorialId,
            int tutorialTypeId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        LecturerDataTransfer GetLecturerByFirstMiddleLastname(string arg);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<LecturerDataTransfer> GetLecturersByFirstMiddleLastname(string arg);
        #endregion

        

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ScheduleInfoDataTransfer GetScheduleInfoById(int id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForFaculty(
            int facultyId,
            int courseId,
            int studyYear,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroups(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleInfoDataTransfer> GetScheduleInfoesForDepartment(
            DepartmentDataTransfer departmentDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleInfoDataTransfer> GetUnscheduledInfoes(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            GroupDataTransfer groupDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        int CountScheduleCollisions(
            int day,
            TimeDataTransfer timeDataTransfer,
            WeekTypeDataTransfer weekTypeDataTransfer);

        #region schedule
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForGroups(
            int facultyId,
            int courseId,
            int[] groupIds,
            int studyYear,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(int scheduleInfoId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesByDayTime(
            int day,
            TimeDataTransfer timeDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForFaculty(
            int facultyId,
            int courseId,
            int studyYearId,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        int CountSchedulesForScheduleInfoes(int scheduleInfoId);


        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForSpeciality(
            int specialityId,
            int courseId,
            int studyYearId,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForLecturer(
            int lecturerId,
            int studyYearId,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            int auditoriumId,
            int studyYearId,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ScheduleDataTransfer GetScheduleById(int id);
        #endregion

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<SpecialityDataTransfer> GetSpecialities(int branchId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<SpecialityDataTransfer> GetSpecialitiesForFaculti(int facultyId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TimeDataTransfer> GetTimes(int buildingId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TutorialDataTransfer GetTutorialById(TutorialDataTransfer tutorialDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TutorialDataTransfer> GetTutorialsForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TutorialDataTransfer> GetTutorialsForSpeciality(
            int courseId,
            int specialityId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TutorialDataTransfer> GetTutorialsForFaculty(
            int facultyId,
            int courseId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TutorialTypeDataTransfer> GetTutorialTypes();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TutorialTypeDataTransfer GetTutorialTypeById(TutorialTypeDataTransfer tutorialTypeDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<WeekTypeDataTransfer> GetWeekTypes();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<StudyYearDataTransfer> GetStudyYears();
    }
}
