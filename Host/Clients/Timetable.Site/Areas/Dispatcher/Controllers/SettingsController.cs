using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Timetable.Data.Models.Personalization;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SettingsController : AuthorizedController
    {
        public ActionResult Index()
        {
            var model = new SettingsViewModel
            {
                Login = UserData.Login,
                Firstname = UserData.Firstname,
                Middlename = UserData.Middlename,
                Lastname = UserData.Lastname,
                Users = UserService
                    .GetUsers(new [] { UserData.Login })
                    .Select(x => new UserViewModel(x))
            };

            return View("_Index", model);
        }

        public ActionResult Save(SaveSettingsRequest request)
        {
            UserData.Firstname = request.Firstname;
            UserData.Middlename = request.Middlename;
            UserData.Lastname = request.Lastname;

            if (request.IsNeedPasswordUpdate())
            {
                if (request.Password != request.ConfirmPassword)
                    return new JsonNetResult(new FailResponse("Пароли не совпадают"));

                UserService.SaveUserState(UserData, request.Password);
            }
            else
                UserService.SaveUserState(UserData);

            return new JsonNetResult(new SettingsUpdateResponse{ UserName = UserData.GetUserName() });
        }

        public ActionResult CreateEditUser(CreateEditUserRequest request)
        {
            if(request.Password != request.ConfirmPassword)
                return new JsonNetResult(new FailResponse("Пароли не совпадают"));

            try
            {
                UserService.CreateUser(
                    request.Login,
                    request.Password,
                    request.Firstname,
                    request.Middlename,
                    request.Lastname,
                    UserRoleType.Dispatcher);
            }
            catch (DuplicateNameException ex)
            {
                return new JsonNetResult(new FailResponse("Этот логин уже занят"));
            }
            
            return new JsonNetResult(new SuccessResponse());
        }



        public ActionResult CreateEditAuditorium(CreateEditAuditoriumRequest request)
        {
            if (request.AuditoriumId.HasValue)
            {
                DataService.EditAuditorium(
                        request.Number,
                        request.Name,
                        request.Info,
                        request.Capacity,
                        request.BuildingId,
                        request.AuditoriumTypeId,
                        request.AuditoriumId.Value);
            }
            else
            {
                DataService.CreateAuditorium(
                        request.Number,
                        request.Name,
                        request.Info,
                        request.Capacity,
                        request.BuildingId,
                        request.AuditoriumTypeId);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteAuditorium(int auditoriumId)
        {
            DataService.DeleteAuditorium(auditoriumId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult GetAuditoriumCreateEditModalData()
        {
            var model = new AuditoriumCreateEditeModalDataResponse
            {
                Buildings = DataService.GetBuildings().Select(x => new BuildingViewModel(x)).ToList(),
                AuditoriumTypes = DataService.GetAuditoriumTypes().Select(x => new AuditoriumTypeViewModel(x)).ToList()
            };
            return new JsonNetResult(model);
            
        }

        public ActionResult GetBuildings()
        {
            var model = new BuildingsInAuditoriumCreateEditResponse
            {
                Buildings = DataService.GetBuildings().Select(x => new BuildingViewModel(x)).ToList(),
                CurrentBuildingId = UserData.DataSettings.AuditoriumsSelectedBuildingId
            };
            return new JsonNetResult(model);
        }

        public ActionResult GetBranches()
        {
            var model = new BranchesInCourseCreateEditResponse
            {
                Branches = DataService.GetBranches().Select(x => new BranchViewModel(x)).ToList(),
                CurrentBranchId = 0//UserData.DataSettings.AuditoriumsSelectedBuildingId
            };
            return new JsonNetResult(model);
        }

        public ActionResult GetCourseCreateEditModalData()
        {
            var model = new CourseCreateEditModalDataResponse
            {
                Branches = DataService.GetBranches().Select(x => new BranchViewModel(x)).ToList(),
           
            };
            return new JsonNetResult(model);

        }

        public ActionResult GetTimeCreateEditModalData()
        {
            var model = new TimeCreateEditModalDataResponse
            {
                Buildings = DataService.GetBuildings().Select(x => new BuildingViewModel(x)).ToList(),
            
            };
            return new JsonNetResult(model);
        }

        public ActionResult GetLecturerCreateEditModalData()
        {
            var model = new LecturerCreateEditModalDataResponse
            {
                Departments = DataService.GetDepartments().Select(x => new DepartmentViewModel(x)).ToList(),
                Positions = DataService.GetPositions().Select(x => new PositionViewModel(x)).ToList()
            };
            return new JsonNetResult(model);
        }

        public ActionResult GetAuditoriums(int buildingId, int pageNumber, int pageSize)
        {
            UserData.DataSettings.AuditoriumsSelectedBuildingId = buildingId;
            UserService.SaveUserState(UserData);

            var auditoriums = DataService.GetAuditoriums(buildingId).Select(x => new AuditoriumViewModel(x)).ToList();
            var model = auditoriums;
            return new JsonNetResult(model);
        }

        public ActionResult GetAuditoriumTypes()
        {
            var auditoriumTypes = DataService.GetAuditoriumTypes().Select(x => new AuditoriumTypeViewModel(x)).ToList();
            var model = auditoriumTypes;
            return new JsonNetResult(model);
        }

        public ActionResult GetWeekTypes()
        {
            var weekTypes = DataService.GetWeekTypes().Select(x => new WeekTypeViewModel(x)).ToList();
            var model = weekTypes;
            return new JsonNetResult(model);
        }

        public ActionResult GetTutorialTypes()
        {
            var tutorialTypes = DataService.GetTutorialTypes().Select(x => new TutorialTypeViewModel(x)).ToList();
            var model = tutorialTypes;
            return new JsonNetResult(model);
        }

        public ActionResult GetScheduleTypes()
        {
            var scheduleTypes = DataService.GetScheduleTypes().Select(x => new ScheduleTypeViewModel(x)).ToList();
            var model = scheduleTypes;
            return new JsonNetResult(model);
        }

        public ActionResult GetSemesters()
        {
            var semesters = DataService.GetSemesters().Select(x => new SemesterViewModel(x)).ToList();
            var model = semesters;
            return new JsonNetResult(model);
        }

        public ActionResult GetStudyYears()
        {
            var studyYears = DataService.GetStudyYears().Select(x => new StudyYearViewModel(x)).ToList();
            var model = studyYears;
            return new JsonNetResult(model);
        }

        public ActionResult GetStudyTypes()
        {
            var studyTypes = DataService.GetStudyTypes().Select(x => new StudyTypeViewModel(x)).ToList();
            var model = studyTypes;
            return new JsonNetResult(model);
        }

        public ActionResult GetCourses(int branchId)
        {
            var courses = DataService.GetCources(branchId).Select(x => new CourseViewModel(x)).ToList();
            var model = courses;
            return new JsonNetResult(model);
        }

        public ActionResult GetTimes(int buildingId)
        {
            var times = DataService.GetTimes(buildingId).Select(x => new TimeViewModel(x)).ToList();
            var model = times;
            return new JsonNetResult(model);
        }

        public ActionResult GetLecturers(string searchString)
        {
            var lecturers = DataService.GetLecturersBySearchString(searchString).Select(x => new LecturerViewModel(x)).ToList();
            var model = lecturers;
            return new JsonNetResult(model);
        }

        public ActionResult GetTutorials(string searchString)
        {
            var tutorials = DataService.GetTutorialsBySearchString(searchString).Select(x => new TutorialViewModel(x)).ToList();
            var model = tutorials;
            return new JsonNetResult(model);
        }

        public ActionResult GetGroups(int facultyId, int courseId)
        {
            var groups = DataService.GetGroupsForFaculty(facultyId,courseId).Select(x => new GroupViewModel(x)).ToList();
            var model = groups;
            return new JsonNetResult(model);
        }

        public ActionResult GetFaculties(int branchId)
        {
            var faculties = DataService.GetFaculties(branchId).Select(x => new FacultyViewModel(x)).ToList();
            var model = faculties;
            return new JsonNetResult(model);
        }

        public ActionResult GetScheduleInfoes(int facultyId, int courseId, int studyYearId, int semesterId)
        {
            var scheduleInfoes = DataService.GetScheduleInfoesForFaculty(facultyId, courseId, studyYearId, semesterId).Select(x => new ScheduleInfoViewModel(x)).ToList();
            var model = scheduleInfoes;
            return new JsonNetResult(model);
        }

        public ActionResult CreateEditAuditoriumType(CreateEditAuditoriumTypeRequest request)
        {
        
            if (request.AuditoriumTypeId.HasValue)
            {
                DataService.EditAuditoriumType(
                        request.Name,
                        request.Pattern,
                        request.AuditoriumTypeId.Value);
            }
            else
            {
                DataService.CreateAuditoriumType(
                        request.Name,
                        request.Pattern);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteAuditoriumType(int auditoriumTypeId)
        {
            DataService.DeleteAuditoriumType(auditoriumTypeId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditWeekType(CreateEditWeekTypeRequest request)
        {

            if (request.WeekTypeId.HasValue)
            {
                DataService.EditWeekType(
                        request.Name,
                        request.WeekTypeId.Value);
            }
            else
            {
                DataService.CreateWeekType(
                        request.Name);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteWeekType(int weekTypeId)
        {
            DataService.DeleteWeekType(weekTypeId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditTutorialType(CreateEditTutorialTypeRequest request)
        {

            if (request.TutorialTypeId.HasValue)
            {
                DataService.EditTutorialType(
                        request.Name,
                        request.TutorialTypeId.Value);
            }
            else
            {
                DataService.CreateTutorialType(
                        request.Name);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteTutorialType(int tutorialTypeId)
        {
            DataService.DeleteTutorialType(tutorialTypeId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditScheduleType(CreateEditScheduleTypeRequest request)
        {

            if (request.ScheduleTypeId.HasValue)
            {
                DataService.EditScheduleType(
                        request.Name,
                        request.ScheduleTypeId.Value);
            }
            else
            {
                DataService.CreateScheduleType(
                        request.Name);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteScheduleType(int scheduleTypeId)
        {
            DataService.DeleteScheduleType(scheduleTypeId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditSemester(CreateEditSemesterRequest request)
        {

            if (request.SemesterId.HasValue)
            {
                DataService.EditSemester(
                        request.Name,
                        request.SemesterId.Value);
            }
            else
            {
                DataService.CreateSemester(
                        request.Name);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteSemester(int semesterId)
        {
            DataService.DeleteSemester(semesterId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditStudyYear(CreateEditStudyYearRequest request)
        {

            if (request.StudyYearId.HasValue)
            {
                DataService.EditStudyYear(
                        int.Parse(request.StartYear),
                        request.Length,
                        request.StudyYearId.Value);
            }
            else
            {
                DataService.CreateStudyYear(
                        int.Parse(request.StartYear),
                        request.Length);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteStudyYear(int StudyYearId)
        {
            DataService.DeleteStudyYear(StudyYearId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditStudyType(CreateEditStudyTypeRequest request)
        {

            if (request.StudyTypeId.HasValue)
            {
                DataService.EditStudyType(
                        request.Name,
                        request.StudyTypeId.Value);
            }
            else
            {
                DataService.CreateStudyType(
                        request.Name);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteStudyType(int studyTypeId)
        {
            DataService.DeleteStudyType(studyTypeId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditCourse(CreateEditCourseRequest request)
        {
            var branchIds = GetListFromString(request.BranchIds).ToArray();

            if (request.CourseId.HasValue)
            {
                DataService.EditCourse(
                        request.Name,
                        branchIds,
                        request.CourseId.Value);
            }
            else
            {
                DataService.CreateCourse(
                        request.Name,
                        branchIds);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteCourse(int courseId)
        {
            DataService.DeleteCourse(courseId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditTime(CreateEditTimeRequest request)
        {
            var buildingIds = GetListFromString(request.BuildingIds).ToArray();

            if (request.TimeId.HasValue)
            {
                DataService.EditTime(
                        request.Start,
                        request.End,
                        request.Position,
                        buildingIds,
                        request.TimeId.Value);
            }
            else
            {
                DataService.CreateTime(
                        request.Start,
                        request.End,
                        request.Position,
                        buildingIds);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteTime(int timeId)
        {
            DataService.DeleteTime(timeId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditLecturer(CreateEditLecturerRequest request)
        {
            var departmentIds = GetListFromString(request.DepartmentIds).ToArray();
            var positionIds = GetListFromString(request.PositionIds).ToArray();

            if (request.LecturerId.HasValue)
            {
                DataService.EditLecturer(
                        request.Firstname,
                        request.Middlename,
                        request.Lastname,
                        request.Contacts,
                        positionIds,
                        departmentIds,
                        request.LecturerId.Value);
            }
            else
            {
                DataService.CreateLecturer(
                        request.Firstname,
                        request.Middlename,
                        request.Lastname,
                        request.Contacts,
                        positionIds,
                        departmentIds);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteLecturer(int lecturerId)
        {
            DataService.DeleteLecturer(lecturerId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditTutorial(CreateEditTutorialRequest request)
        {


            if (request.TutorialId.HasValue)
            {
                DataService.EditTutorial(
                        request.Name,
                        request.ShortName,
                        request.TutorialId.Value);
            }
            else
            {
                DataService.CreateTutorial(
                        request.Name,
                        request.ShortName);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteTutorial(int tutorialId)
        {
            DataService.DeleteTutorial(tutorialId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditGroup(CreateEditGroupRequest request)
        {
            var facultyIds = GetListFromString(request.FacultyIds).ToArray();
            var courseIds = GetListFromString(request.CourseIds).ToArray();

            if (request.GroupId.HasValue)
            {
                DataService.EditGroup(
                        request.Code,
                        request.StudentsCount,
                        facultyIds,
                        courseIds,
                        request.StudyTypeId,
                        request.GroupId.Value);
            }
            else
            {
                DataService.CreateGroup(
                        request.Code,
                        request.StudentsCount,
                        facultyIds,
                        courseIds,
                        request.StudyTypeId);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteGroup(int groupId)
        {
            DataService.DeleteGroup(groupId);
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult CreateEditScheduleInfoes(CreateEditScheduleInfoRequest request)
        {
            var facultyIds = GetListFromString(request.FacultyIds).ToArray();
            var courseIds = GetListFromString(request.CourseIds).ToArray();
            var groupIds = GetListFromString(request.CourseIds).ToArray();

            if (request.ScheduleInfoId.HasValue)
            {
                DataService.EditScheduleInfo(
                        request.SubGroupCount,
                        request.HoursPerWeek,
                        request.StartDate,
                        request.EndDate,
                        facultyIds,
                        courseIds,
                        groupIds,
                        request.LecturerSearchString,
                        request.SemesterId,
                        request.DepartmentId,
                        request.StudyYearId,
                        request.TutorialSearchString,
                        request.TutorialTypeId,
                        request.ScheduleInfoId.Value);
            }
            else
            {
                DataService.CreateScheduleInfo(
                        request.SubGroupCount,
                        request.HoursPerWeek,
                        request.StartDate,
                        request.EndDate,
                        facultyIds,
                        courseIds,
                        groupIds,
                        request.LecturerSearchString,
                        request.SemesterId,
                        request.DepartmentId,
                        request.StudyYearId,
                        request.TutorialSearchString,
                        request.TutorialTypeId);
            }
            return new JsonNetResult(new SuccessResponse());
        }

        public ActionResult DeleteScheduleInfo(int scheduleInfoId)
        {
            DataService.DeleteScheduleInfo(scheduleInfoId);
            return new JsonNetResult(new SuccessResponse());
        }
    }
}
