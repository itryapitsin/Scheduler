using System;
using System.Collections.Generic;
using Timetable.Logic.Exceptions;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Logic.Interfaces
{
    public interface IDataService : IBaseService
    {
        IEnumerable<SemesterDataTransfer> GetSemesters();
        SemesterDataTransfer GetSemesterForTime(DateTime? date);

        IEnumerable<GroupDataTransfer> GetAllGroupsForFaculty(int facultyId, int courseId, int studyTypeId);
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
            int? weekTypeId,
            int timeId,
            int? scheduleId);
        IEnumerable<AuditoriumTypeDataTransfer> GetAuditoriumTypes(bool? isTraining = null);
        IEnumerable<BuildingDataTransfer> GetBuildings();
        IEnumerable<CourseDataTransfer> GetCources(int branchId);
        IEnumerable<int> GetPairs();
        IEnumerable<FacultyDataTransfer> GetFaculties(BranchDataTransfer branchDataTransfer = null);
        IEnumerable<FacultyDataTransfer> GetFaculties(int branchId);
        FacultyDataTransfer GetFacultyById(int facultyId);

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
        LecturerDataTransfer GetLecturerBySearchString(string searchString);
        IEnumerable<LecturerDataTransfer> GetLecturersBySearchString(string searchString);
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
            int weekTypeId,
            int auditoriumId,
            int scheduleInfoId,
            int? scheduleId);

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
            int[] auditoriumIds,
            int studyYear,
            int semester,
            int timeId,
            int dayOfWeek);
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
        IEnumerable<ScheduleDataTransfer> GetSchedulesForSchedule(int scheduleId);

        IEnumerable<ScheduleDataTransfer> GetSchedulesForScheduleInfo(int scheduleInfoId);
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
        /// Редактировать запланированный предмет.
        /// </summary>
        /// <exception cref="ScheduleCollisionException"></exception>
        /// <param name="auditoriumId"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="scheduleId"></param>
        /// <param name="timeId"></param>
        /// <param name="weekTypeId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        ScheduleDataTransfer PlanEdit(
            int auditoriumId,
            int dayOfWeek,
            int scheduleId,
            int timeId,
            int weekTypeId,
            int typeId,
            string subGroup);

        void Unplan(int scheduleId);

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
            int typeId,
            string subGroup);

        void EditAuditorium(
             string number,
             string name,
             string info,
             int? capacity,
             int buildingId,
             int auditoriumTypeId,
             int auditoriumId
             );

       void CreateAuditorium(
            string number,
            string name,
            string info,
            int? capacity,
            int buildingId,
            int auditoriumTypeId
            );

       void DeleteAuditorium(int auditoriumId);

       void EditAuditoriumType(
          string name,
          string pattern,
          int auditoriumTypeId
          );

       void CreateAuditoriumType(
              string name,
              string pattern
           );

       void DeleteAuditoriumType(int auditoriumTypeId);

       void EditWeekType(
          string name,
          int weekTypeId
          );

       void CreateWeekType(
                      string name
                   );

       void DeleteWeekType(int weekTypeId);

       void EditStudyYear(
                   int startYear,
                   int length,
                   int studyYearId
                  );

       void CreateStudyYear(
                       int startYear,
                       int length
                   );

       void DeleteStudyYear(int studyYearId);

       void EditSemester(
                  string name,
                  int semesterId
                  );

       void CreateSemester(
                      string name
                   );

       void DeleteSemester(int semesterId);

       void EditScheduleType(
                  string name,
                  int scheduleTypeId
                  );

       void CreateScheduleType(
                      string name
                   );

       void DeleteScheduleType(int scheduleTypeId);


       void EditTutorialType(
                  string name,
                  int tutorialTypeId
                  );

       void CreateTutorialType(
                      string name
                   );

       void DeleteTutorialType(int tutorialTypeId);

       void EditStudyType(
                 string name,
                 int studyTypeId
                 );

       void CreateStudyType(
                      string name
                   );

       void DeleteStudyType(int studyTypeId);


       void EditCourse(
                string name,
                int[] branchIds,
                int courseId
                );

       void CreateCourse(
                      string name,
                      int[] branchIds
                   );

       void DeleteCourse(int courseId);

       void EditTime(
               string start,
               string end,
               int position,
               int[] buildingIds,
               int timeId
               );

       void CreateTime(
                      string start,
                      string end,
                      int position,
                      int[] buildingIds
                   );

       void DeleteTime(int timeId);

       void EditLecturer(
           string firstName,
           string middleName,
           string lastName,
           string contacts,
           int[] positionIds,
           int[] departmentIds,
           int lecturerId
           );

       void CreateLecturer(
          string firstName,
          string middleName,
          string lastName,
          string contacts,
          int[] positionIds,
          int[] departmentIds
          );

        void DeleteLecturer(int lecturerId);

        void EditTutorial(
           string name,
           string shortName,
           int tutorialId
           );

        void CreateTutorial(
           string name,
           string shortName
           );

        void DeleteTutorial(int tutorialId);

        void EditGroup(
            string code,
            int studentsCount,
            int [] facultyIds,
            int [] courseIds,
            int studyTypeId,
            int groupId,
            bool isActual
           );

        void CreateGroup(
            string code,
            int studentsCount,
            int [] facultyIds,
            int [] courseIds,
            int studyTypeId,
            bool isActual
           );

        void EditScheduleInfo(
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
          );

       void CreateScheduleInfo(
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
            );

        void DeleteScheduleInfo(int scheduleInfoId);

        void DeleteGroup(int groupId);

        void EditSchedule(
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
          );

        void CreateSchedule(
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
            );

        void DeleteSchedule(int scheduleId);

       AuditoriumOrderDataTransfer PlanAuditoriumOrder(
                string tutorialName,
                string lecturerName,
                string threadName,
                int dayOfWeek,
                int timeId,
                int auditoriumId,
                bool autoDelete
            );
        void EditAuditoriumOrder(
                string tutorialName,
                string lecturerName,
                string threadName,
                int auditoriumOrderId,
                bool autoDelete
            );
        void UnplanAuditoriumOrder(
                int auditoriumOrderId
            );

        IEnumerable<AuditoriumOrderDataTransfer> GetAuditoriumOrders(
            int timeId,
            int dayOfWeek,
            int buildingId);

        IEnumerable<AuditoriumOrderDataTransfer> GetAuditoriumOrders(
            int timeId,
            int dayOfWeek,
            int buildingId,
            int[] auditoriumTypeIds = null);

        void PlanExam(
                int? lecturerId,
                int tutorialId,
                int? groupId,
                int? auditoriumId,
                DateTime time
            );

        void UnplanExam(
                int examId
            );

        IEnumerable<DepartmentDataTransfer>  GetDepartments();

        IEnumerable<PositionDataTransfer> GetPositions();

        IEnumerable<TutorialDataTransfer> GetTutorialsBySearchString(string searchString);

    }
}
