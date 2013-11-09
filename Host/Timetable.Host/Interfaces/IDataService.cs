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
    public interface IDataService: IBaseService
    {

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForDayTimeDate(
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
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForAll(
                                LecturerDataTransfer lecturerDataTransfer,
                                AuditoriumDataTransfer auditoriumDataTransfer,
                                IEnumerable<GroupDataTransfer> groups,
                                WeekTypeDataTransfer weekTypeDataTransfer,
                                string subGroup,
                                DateTime startDate,
                                DateTime endDate
                                );


        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForGroupOnlyId(
           GroupDataTransfer groupDataTransfer,
           StudyYearDataTransfer studyYearDataTransfer,
           int semester,
           DateTime startDate,
           DateTime endDate);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<TimetableEntityDataTransfer> GetTimetableEntities();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<GroupDataTransfer> GetGroupsByCode(string code, int count);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        bool ValidateSchedule(ScheduleDataTransfer scheduleDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<BranchDataTransfer> GetBranches();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<AuditoriumDataTransfer> GetAuditoriums(
            BuildingDataTransfer buildingDataTransfer,
            AuditoriumTypeDataTransfer auditoriumTypeDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<AuditoriumDataTransfer> GetFreeAuditoriums(
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
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<AuditoriumTypeDataTransfer> GetAuditoriumTypes();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<BuildingDataTransfer> GetBuildings();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<CourseDataTransfer> GetCources();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<DepartmentDataTransfer> GetDeparmtents();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        GroupDataTransfer GetGroupById(
            int groupId);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<GroupDataTransfer> GetsSubGroupsByGroupId(
            int groupId);

        [OperationContract(Name = "GetGroupsForCourse")]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<GroupDataTransfer> GetGroups(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer);

        [OperationContract(Name = "GetGroupsForSpeciality")]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<GroupDataTransfer> GetGroups(
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<LecturerDataTransfer> GetLecturersByDeparmentId(
            DepartmentDataTransfer departmentDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<LecturerDataTransfer> GetLecturersByTutorialId(
            TutorialDataTransfer tutorialDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<LecturerDataTransfer> GetLecturersByTutorialIdAndTutorialTypeId(
            TutorialDataTransfer tutorialDataTransfer,
            TutorialTypeDataTransfer tutorialTypeDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        LecturerDataTransfer GetLecturerByFirstMiddleLastname(
            string arg);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<LecturerDataTransfer> GetLecturersByFirstMiddleLastname(
            string arg);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForScheduleInfoes(ScheduleInfoDataTransfer scheduleInfoDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ScheduleInfoDataTransfer GetScheduleInfoById(
            int id);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer, 
            int semester);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer,
            TutorialTypeDataTransfer tutorialtype,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleInfoDataTransfer> GetScheduleInfoesForDepartment(
            DepartmentDataTransfer departmentDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleInfoDataTransfer> GetUnscheduledInfoes(
            FacultyDataTransfer facultyDataTransfer, 
            CourseDataTransfer courseDataTransfer, 
            SpecialityDataTransfer specialityDataTransfer, 
            GroupDataTransfer groupDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        int CountScheduleCollisions(
            int day, 
            TimeDataTransfer timeDataTransfer, 
            WeekTypeDataTransfer weekTypeDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesByDayTime(
            int day,
            TimeDataTransfer timeDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime StartDate,
            DateTime EndDate);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate,
            string subGroup);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        int CountSchedulesForScheduleInfoes(ScheduleInfoDataTransfer scheduleInfoDataTransfer);


        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForLecturer(
            LecturerDataTransfer lecturerDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<ScheduleDataTransfer> GetSchedulesForAuditorium(
            AuditoriumDataTransfer auditoriumDataTransfer,
            StudyYearDataTransfer studyYearDataTransfer,
            int semester,
            DateTime startDate,
            DateTime endDate);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ScheduleDataTransfer GetScheduleById(int id);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<SpecialityDataTransfer> GetSpecialities(
            FacultyDataTransfer facultyDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<TimeDataTransfer> GetTimes(BuildingDataTransfer buildingDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TutorialDataTransfer GetTutorialById(TutorialDataTransfer tutorialDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<TutorialDataTransfer> GetTutorialsForGroup(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            GroupDataTransfer groupDataTransfer);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<TutorialDataTransfer> GetTutorialsForSpeciality(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer,
            SpecialityDataTransfer specialityDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<TutorialDataTransfer> GetTutorialsForCourse(
            FacultyDataTransfer facultyDataTransfer,
            CourseDataTransfer courseDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<TutorialTypeDataTransfer> GetTutorialTypes();

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TutorialTypeDataTransfer GetTutorialTypeById(TutorialTypeDataTransfer tutorialTypeDataTransfer);

        [OperationContract]
        [ApplyDataContractResolver]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<WeekTypeDataTransfer> GetWeekTypes();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IQueryable<StudyYearDataTransfer> GetStudyYears();
    }
}
