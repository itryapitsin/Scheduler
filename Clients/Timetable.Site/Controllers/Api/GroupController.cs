using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.Models.Groups;
using Course = Timetable.Site.DataService.Course;
using Faculty = Timetable.Site.DataService.Faculty;
using Group = Timetable.Site.DataService.Group;
using GroupViewModel = Timetable.Site.Models.Groups.GroupViewModel;
using Speciality = Timetable.Site.DataService.Speciality;

namespace Timetable.Site.Controllers.Api
{
    public partial class GroupController : BaseApiController<Group>
    {
        //Получить все группы, чей код попадает под маску 
        public HttpResponseMessage GetByCode(string code)
        {
            var result = NewDataService
                .GetGroupsByCode(code, 10)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        //Получить все группы по факультету, курсу и специальности (обзательно должен быть выбран факультет и курс, 
        //специальность может не выбираться
        public HttpResponseMessage GetAll(int facultyId, string courseIds, string specialityIds)
        {
            var ids = courseIds.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = NewDataService
                .GetGroupsForCourses(
                    facultyId,
                    ids)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Получить группы по специальностям
        public HttpResponseMessage GetBySpecialities(int courseId, string specialityIds)
        {
            var ids = specialityIds.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = NewDataService
                .GetGroupsForSpecialities(courseId, ids)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public IEnumerable<GroupViewModel> privateGetBySpecialities(string specialityIds)
        {
            var result = new List<GroupViewModel>();

            var courseController = new CourseController();
            var courses = courseController.privateGetAll();

            foreach (var cc in courses)
            {
                var qCourse = new Course();
                qCourse.Id = cc.Id;
                foreach (var specialityId in specialityIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (specialityId != " ")
                    {
                        int ispecialityId = int.Parse(specialityId);
                        var qSpeciality = new Speciality();
                        qSpeciality.Id = ispecialityId;


                        var tmp = DataService.GetGroupsForSpeciality(qCourse, qSpeciality);
                        foreach (var t in tmp)
                        {
                            result.Add(new GroupViewModel(t));
                        }
                    }
                }
            }

            return result;
        }

        //Получить группы по курсам
        public HttpResponseMessage GetByCourses(int facultyId, string courseIds)
        {
            return CreateResponse<int, string, IEnumerable<GroupViewModel>>(privateGetByCourses, facultyId, courseIds);
        }

        public IEnumerable<GroupViewModel> privateGetByCourses(int facultyId, string courseIds)
        {
            var result = new List<GroupViewModel>();

            var qFaculty = new Faculty();
            qFaculty.Id = facultyId;

            foreach (var courseId in courseIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (courseId != " ")
                {
                    int icourseId = int.Parse(courseId);
                    var qCourse = new Course();
                    qCourse.Id = icourseId;
                    var tmp = DataService.GetGroupsForCourse(qFaculty, qCourse);
                    foreach (var t in tmp)
                    {
                        result.Add(new GroupViewModel(t));
                    }
                }
            }

            return result;
        }


        //Получить группы по курсу
        public HttpResponseMessage GetByCourse(int facultyId, int courseId)
        {
            return CreateResponse<int, int, IEnumerable<GroupViewModel>>(privateGetByCourse, facultyId, courseId);
        }

        private IEnumerable<GroupViewModel> privateGetByCourse(int facultyId, int courseId)
        {
            var result = new List<GroupViewModel>();

            var qFaculty = new Faculty();
            var qCourse = new Course();
            qFaculty.Id = facultyId;
            qCourse.Id = courseId;

            var tmp = DataService.GetGroupsForCourse(qFaculty, qCourse);
            //var tmp = GetTempGroups(qCourse, null);
            foreach (var t in tmp)
            {
                result.Add(new GroupViewModel(t));
            }

            return result;
        }

        public IEnumerable<GroupViewModel> GetByIds(string groupIds)
        {
            var result = new List<GroupViewModel>();

            if (groupIds != null)
            {
                foreach (var groupId in groupIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (groupId != " ")
                    {
                        var tmp = DataService.GetGroupById(int.Parse(groupId));
                        //var tmp = GetTempGroupById(int.Parse(groupId));
                        result.Add(new GroupViewModel(tmp));
                    }
                }
            }

            return result;
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage Add(AddModel model)
        {
            return CreateResponse(privateAdd, model);
        }

        public void privateAdd(AddModel model)
        {
            var aGroup = new Group();

            aGroup.Code = model.Code;
            aGroup.StudentsCount = model.StudentsCount;

            aGroup.Course = new Course();
            aGroup.Course.Id = model.CourseId;
            aGroup.Parent = new Group();
            aGroup.Parent.Id = model.ParentId;
            aGroup.Speciality = new Speciality();
            aGroup.Speciality.Id = model.SpecialityId;


            aGroup.UpdateDate = DateTime.Now.Date;
            aGroup.CreatedDate = DateTime.Now.Date;
            aGroup.IsActual = true;

            DataService.Add(aGroup);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(DeleteModel model)
        {
            return CreateResponse(privateDelete, model.Id);
        }

        public void privateDelete(int Id)
        {
            var dGroup = new Group();
            dGroup.Id = Id;
            DataService.Delete(dGroup);
        }
    }



}